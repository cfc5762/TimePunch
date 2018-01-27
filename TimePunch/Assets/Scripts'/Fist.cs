using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Fist : MonoBehaviour {
    int punchtimer = 0;
    RaycastHit info;
    Vector3 prevLocalPos;
    Vector3 prevpos;
    public static Vector3 RightHit;
    public static Vector3 LeftHit;
    public bool rightFist;
    public GameObject hand;
    public float maxDist;
    public float maxSpeed;
    Vector3 idealPoint;
    SteamVR_Controller.Device cont;
    // Use this for initialization
    void Start () {
       
        info = new RaycastHit();
        prevpos = Vector3.zero;
        RightHit = Vector3.zero;
        LeftHit = Vector3.zero;
        idealPoint = new Vector3();
        if (rightFist == null)
            rightFist = true;
        hand = this.transform.parent.gameObject;
      
       
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (cont == null)
        {
            cont = GetComponentInParent<Hand>().controller;
        }
        else
        {
            if (punchtimer > 0)
            {
                idealPoint = new Vector3(0, -maxDist / 2, -.065f + maxDist / 2);
                punchtimer--;
            }
            else
            {
                if (cont.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
                {
                    punchtimer = 20;
                }
                idealPoint = new Vector3(0, 0, -.065f);
            }

            

            transform.localPosition = transform.localPosition + Vector3.ClampMagnitude(idealPoint - transform.localPosition, maxSpeed);

            if (cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis2).x < .01 && Physics.Raycast(transform.position, transform.forward - transform.up, out info, .2f))
            {

                if (info.transform.tag != "enemy"&&info.transform.tag != "Fist")
                {
                    print(info.transform.name);
                    rigidScript.Rig3D.AddForce(((transform.forward - transform.up).normalized * -(prevpos - transform.position).magnitude / Time.deltaTime * 400f));
                }
            }
            if (!rightFist)
            {

                if (cont.GetState().rAxis0.x >= .1f || cont.GetState().rAxis0.x <= -.1f)
                {

                    rigidScript.Rig3D.transform.Rotate(new Vector3(0, 1, 0), cont.GetState().rAxis0.x * 4f);
                }
               
            }
            else
            {
                
                rigidScript.Rig3D.AddForce(rigidScript.Rig3D.transform.forward* cont.GetState().rAxis0.y * 90f);
                rigidScript.Rig3D.AddForce(rigidScript.Rig3D.transform.right * cont.GetState().rAxis0.x * 90f);
            }
           
            

        }
        
        
        rigidScript.Rig3D.velocity = Vector3.ClampMagnitude(rigidScript.Rig3D.velocity, 12);
        prevLocalPos = transform.localPosition;
        prevpos = transform.position;

    }
   
   
}

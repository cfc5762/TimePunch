using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Fist : MonoBehaviour {

    public static Fist rFist;
    public static Fist lFist;
    int punchwaiter = 0;
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
            if (rFist == this)
            {
              

               
                
                if (cont.GetState().rAxis0.x >= .1f || cont.GetState().rAxis0.x <= -.1f)
                {

                    rigidScript.Rig3D.transform.Rotate(new Vector3(0, 1, 0), cont.GetState().rAxis0.x * 2f);
                }
                transform.GetChild(0).localScale = new Vector3(-50, -50, 50);
            }
            else
            {
                Vector3 vel = rigidScript.Rig3D.velocity;

                if (4 > Vector3.Dot(vel.normalized, (vel + (rigidScript.Rig3D.transform.right * cont.GetState().rAxis0.x * 1f) + (rigidScript.Rig3D.transform.forward * cont.GetState().rAxis0.y * 1f))))
                {
                    rigidScript.Rig3D.velocity = rigidScript.Rig3D.velocity + rigidScript.Rig3D.transform.right * cont.GetState().rAxis0.x * 1f;
                    rigidScript.Rig3D.velocity = rigidScript.Rig3D.velocity + rigidScript.Rig3D.transform.forward * cont.GetState().rAxis0.y * 1f;
                }
                else
                {
                    Vector3.ClampMagnitude(rigidScript.Rig3D.velocity + rigidScript.Rig3D.transform.forward * cont.GetState().rAxis0.y * 1f + rigidScript.Rig3D.transform.right * cont.GetState().rAxis0.x * 1f,6);
                }
               
                transform.GetChild(0).localScale = new Vector3(-50, -50, -50);
            }
            if (lFist == null)
            {
                if (cont == SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestLeft)))
                {
                    lFist = this;
                }
                if (cont == SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestRight)))
                {
                    lFist = rFist;
                    rFist = this;

                }
            }
            if (rFist == null)
            {
                if (cont == SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestRight)))
                {
                    rFist = this;
                }
                if (cont == SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestLeft)))
                {
                    rFist = rFist;
                    lFist = this;

                }
            }
            if (cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis2).x>.01f)
            {
                rigidScript.Rig3D.mass = 100;
                
            }
            else
            {
                rigidScript.Rig3D.mass = 10;
                
            }
            if (punchtimer > 0)
            {
                idealPoint = new Vector3(0, -maxDist / 2, -.065f + maxDist / 2);
                punchtimer--;
                punchwaiter = 20;
            }
            else
            {
               
                if (punchwaiter > 0)
                {
                    punchwaiter--;
                }
                else if (cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x>.01f)
                {
                    punchtimer = 20;
                }
                idealPoint = new Vector3(0, 0, -.065f);
            }

            

            transform.localPosition = transform.localPosition + Vector3.ClampMagnitude(idealPoint - transform.localPosition, maxSpeed);

            if (cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x > .01 && cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis2).x < .01 && Physics.Raycast(transform.position, transform.forward - transform.up, out info, .3f))
            {

                if (info.transform.tag != "enemy"&&info.transform.name != "Player")
                {
<<<<<<< HEAD
                    cont.TriggerHapticPulse(3000,Valve.VR.EVRButtonId.k_EButton_Axis4);
                    rigidScript.Rig3D.AddForce(((transform.forward - transform.up).normalized * -(prevpos - transform.position).magnitude / Time.deltaTime * 400f*10/6));
                    if (info.collider.gameObject.tag == "Pillar")
                    {
                        rigidScript.Rig3D.AddForce(((transform.forward - transform.up).normalized * -(prevpos - transform.position).magnitude / Time.deltaTime * 400f * 10 / 6));
                    }
=======
                    print(info.transform.name);
                    rigidScript.Rig3D.AddForce(((transform.forward - transform.up).normalized * -(prevpos - transform.position).magnitude / Time.deltaTime * 1200f*10/6));
>>>>>>> master
                }
            }
            
           
            

        }
        
        
        rigidScript.Rig3D.velocity = Vector3.ClampMagnitude(rigidScript.Rig3D.velocity, 20);
        prevLocalPos = transform.localPosition;
        prevpos = transform.position;

    }
   
   
}

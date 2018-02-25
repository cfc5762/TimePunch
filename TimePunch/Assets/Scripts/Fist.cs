using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class Fist : MonoBehaviour {
    bool canpunch;
    public float Acceleration;
    public float MoveSpeed;
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
        canpunch = true;
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
                Vector3 ovel = rigidScript.Rig3D.velocity;
                Vector3 vel = rigidScript.Rig3D.velocity;
                Vector2 axis = new Vector3(cont.GetState().rAxis0.x, cont.GetState().rAxis0.y);
                float y = vel.y;
                ovel.y = 0;
                vel.y = 0;
                print(GroundScript.OnGround);
                vel += Vector3.ClampMagnitude(rigidScript.Rig3D.transform.forward * axis.y + rigidScript.Rig3D.transform.right* axis.x,(GroundScript.OnGround)?Acceleration:.08f)*axis.magnitude;
                if (vel.magnitude > MoveSpeed*axis.magnitude && ovel.magnitude > vel.magnitude)
                {
                    vel.y = y;
                    rigidScript.Rig3D.velocity = vel;
                }
                else if (vel.magnitude > MoveSpeed*axis.magnitude)
                {

                }
                else
                {
                    vel.y = y;
                    rigidScript.Rig3D.velocity = vel;
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
                rigidScript.Rig3D.mass = 15;
                
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
                    canpunch = true; 
                    punchtimer = 20;
                }
                idealPoint = new Vector3(0, 0, -.065f);
            }

            
            
            transform.localPosition = transform.localPosition + Vector3.ClampMagnitude(idealPoint - transform.localPosition, maxSpeed);
           
                if (cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x > .01 && cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis2).x < .01 && Physics.Raycast(transform.position, transform.forward - transform.up, out info, .3f))
                {
                if (info.transform.tag == "playButton")
                {
                    SceneManager.LoadScene("Level1");
                }
                    if (info.transform.tag != "enemy" && info.transform.name != "Player" && canpunch)
                    {
                        canpunch = false;
                        cont.TriggerHapticPulse(3000, Valve.VR.EVRButtonId.k_EButton_Axis4);
                        Vector3 vel = rigidScript.Rig3D.velocity;
                        vel.y = 0;
                        
                        vel+=((transform.forward - transform.up).normalized * -(prevLocalPos - transform.localPosition).magnitude/Time.deltaTime*.55f);
                    rigidScript.Rig3D.velocity = vel;
                        if (info.collider.gameObject.tag == "Pillar")
                        {
                            rigidScript.Rig3D.AddForce(((transform.forward - transform.up).normalized * -(prevpos - transform.position).magnitude / Time.deltaTime * 800f * 2));
                        }



                    }
                }
            
           
            

        }
        
        
        rigidScript.Rig3D.velocity = Vector3.ClampMagnitude(rigidScript.Rig3D.velocity, 25);
        prevLocalPos = transform.localPosition;
        prevpos = transform.position;

    }
   
   
}

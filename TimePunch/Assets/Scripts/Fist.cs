using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class Fist : MonoBehaviour {
    int punchBuffer = 0;
    bool canLaunch;
    bool reSync = false;
    public float Acceleration;
    public float MoveSpeed;
    public static Fist rFist;
    public static Fist lFist;
    int punchWaiter = 0;
    int punchTimer = 0;
    RaycastHit info;
    Vector3 prevLocalPos;
    Vector3 prevpos;
    public static Vector3 RightHit;
    public static Vector3 LeftHit;
    public GameObject hand;
    public float maxDist;
    public float maxSpeed;
    Vector3 idealPoint;
    SteamVR_Controller.Device cont;
    // Use this for initialization
    void Start () {
        canLaunch = true;
        info = new RaycastHit();
        prevpos = Vector3.zero;
        RightHit = Vector3.zero;
        LeftHit = Vector3.zero;
        idealPoint = new Vector3();
        hand = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (cont == null)//set controller
        {
            cont = GetComponentInParent<Hand>().controller;
        }
        else
        {
            if (punchBuffer > 0)
            {
                punchBuffer--;
            }
            if (cont.GetHairTriggerDown())
            {
                punchBuffer = punchWaiter + 1;
            }
            if (cont.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
            {

                lFist = null;
                rFist = null;
            }
           

            if (lFist == null || reSync)
            {
                if (cont == SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestLeft)))
                {
                    lFist = this;
                }
                if (cont == SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestRight)))
                {
                    rFist = lFist;
                    rFist = this;

                }
            }
            if (rFist == null || reSync)
            {
                if (cont == SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestRight)))
                {
                    rFist = this;
                }
                if (cont == SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestLeft)))
                {
                    lFist = rFist;
                    lFist = this;
                }
            }
            if (rFist == this)
            {// right fist stuff
              

               
                
                if (cont.GetState().rAxis0.x >= .1f || cont.GetState().rAxis0.x <= -.1f)//rotate here
                {

                    rigidScript.Rig3D.transform.Rotate(new Vector3(0, 1, 0), cont.GetState().rAxis0.x * 2f);
                }
                transform.GetChild(0).localScale = new Vector3(-50, -50, 50);//boxing glove scale
            }
            else
            {
                //setup for movement
                Vector3 ovel = rigidScript.Rig3D.velocity;
                Vector3 vel = rigidScript.Rig3D.velocity;
                Vector2 axis = new Vector3(cont.GetState().rAxis0.x, cont.GetState().rAxis0.y);
                float y = vel.y;
                ovel.y = 0;
                vel.y = 0;
                
                vel += Vector3.ClampMagnitude(Head.lookDir * axis.y + Head.rightDir* axis.x,(GroundScript.OnGround)?Acceleration:.08f)*axis.magnitude;
                if (vel.magnitude > MoveSpeed*axis.magnitude && ovel.magnitude > vel.magnitude)
                {
                    //this is where we move
                    vel.y = y;
                    rigidScript.Rig3D.velocity = vel;
                }
                else if (vel.magnitude > MoveSpeed*axis.magnitude)
                {
                    //this is so we dont break velocity cap
                }
                else
                {
                    //this is also a valid move condiditon
                    vel.y = y;
                    rigidScript.Rig3D.velocity = vel;
                }
                transform.GetChild(0).localScale = new Vector3(-50, -50, -50);//fist scale
            }
            
          
                rigidScript.Rig3D.mass = 15;
                
            
            if (punchTimer > 0)//this is where we set the fists ideal location to the punch destination
            {
                idealPoint = new Vector3(0, -maxDist / 2, -.065f + maxDist / 2);
                punchTimer--;
                punchWaiter = 20;
            }
            else
            {
               
                if (punchWaiter > 0)//this is where we wait to punch
                {
                    punchWaiter--;
                    if (transform.localPosition == idealPoint&&punchWaiter<19)
                    {
                        punchWaiter = 0;
                    }
                }
                else if (punchBuffer>0)//this is where we punch
                {
                    canLaunch = true; 
                    punchTimer = 20;
                }
                idealPoint = new Vector3(0, 0, -.065f);
            }

            
            
            transform.localPosition = transform.localPosition + Vector3.ClampMagnitude(idealPoint - transform.localPosition, maxSpeed);
           
                if (cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x > .01 && Physics.Raycast(transform.position, transform.forward - transform.up, out info, .3f))
                {
                if (info.transform.tag == "playButton")
                {
                    SceneManager.LoadScene("Level1");
                }
                    if (info.transform.tag != "enemy" && info.transform.name != "Player" && canLaunch)
                    {
                        canLaunch = false;
                        cont.TriggerHapticPulse(3000, Valve.VR.EVRButtonId.k_EButton_Axis4);
                        Vector3 vel = rigidScript.Rig3D.velocity;
                    if ((vel + ((transform.forward - transform.up).normalized * -(prevLocalPos - transform.localPosition).magnitude / Time.deltaTime * .55f)).magnitude < vel.magnitude)
                    {
                        vel += ((transform.forward - transform.up).normalized * -(prevLocalPos - transform.localPosition).magnitude / Time.deltaTime * .55f) * 2;
                    }
                    else
                    {
                        vel += ((transform.forward - transform.up).normalized * -(prevLocalPos - transform.localPosition).magnitude / Time.deltaTime * .55f);
                    }
                    punchTimer = 0;
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

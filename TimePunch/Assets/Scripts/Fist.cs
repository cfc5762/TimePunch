using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class Fist : MonoBehaviour {
    Vector3 inp = Vector3.zero;
    public static int speedImmune = 0;
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
    AudioSource punch = null;
    AudioSource whoosh = null;
    AudioSource wind = null;
    AudioSource foot1 = null;
    AudioSource landing = null;
    float timeForSteps;
    bool wasInAir = false;
    // Use this for initialization
    void Start () {
        AudioSource[] allAudioSources = GetComponents<AudioSource>();
        if (allAudioSources != null)
        {
            foot1 = allAudioSources[0];
            landing = allAudioSources[1];
            wind = allAudioSources[2];
            whoosh = allAudioSources[3];
            punch = allAudioSources[4];
        }
        canLaunch = true;
        info = new RaycastHit();
        prevpos = Vector3.zero;
        RightHit = Vector3.zero;
        LeftHit = Vector3.zero;
        idealPoint = new Vector3();
        hand = this.transform.parent.gameObject;
        if (wind != null)
        {
            wind.volume = 0;
            wind.Play();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (cont == null)//set controller
        {
            cont = GetComponentInParent<Hand>().controller;
        }
        else
        {
            if (speedImmune > 0)
            {
                speedImmune--;
            }
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

                if (speedImmune > 0)
                {
                    speedImmune--;
                }
               
                
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
                
                if (GroundScript.OnGround)
                {
                    if (axis.sqrMagnitude > .01f)
                    {
                        rigidScript.Rig3D.useGravity = false;
                        if (axis.x < 0)//left
                        {
                            if (axis.y < 0)//backwards
                            {
                                vel += Vector3.ClampMagnitude(GroundScript.Back.normalized * Mathf.Abs(axis.y) + GroundScript.Left.normalized * Mathf.Abs(axis.x), (GroundScript.OnGround) ? Acceleration : .08f) * axis.magnitude;
                            }
                            else//forwards
                            {
                                vel += Vector3.ClampMagnitude(GroundScript.Forward.normalized * Mathf.Abs(axis.y) + GroundScript.Left.normalized * Mathf.Abs(axis.x), (GroundScript.OnGround) ? Acceleration : .08f) * axis.magnitude;
                            }
                        }
                        else//right
                        {
                            if (axis.y < 0)//backwards
                            {
                                vel += Vector3.ClampMagnitude(GroundScript.Back.normalized * Mathf.Abs(axis.y) + GroundScript.Right.normalized * Mathf.Abs(axis.x), (GroundScript.OnGround) ? Acceleration : .08f) * axis.magnitude;

                            }
                            else//forwards
                            {
                                vel += Vector3.ClampMagnitude(GroundScript.Forward.normalized * Mathf.Abs(axis.y) + GroundScript.Right.normalized * Mathf.Abs(axis.x), (GroundScript.OnGround) ? Acceleration : .08f) * axis.magnitude;

                            }
                        }
                    }
                    else
                    {
                        rigidScript.Rig3D.useGravity = true;
                    }
                    
                }
                else
                {

                    vel += Vector3.ClampMagnitude(Head.lookDir * axis.y + Head.rightDir * axis.x, (GroundScript.OnGround) ? Acceleration : .12f) * axis.magnitude;

                    vel += Vector3.ClampMagnitude(Head.lookDir * axis.y + Head.rightDir * axis.x, (GroundScript.OnGround) ? Acceleration : .12f) * axis.magnitude;

                }
                if (vel.magnitude > MoveSpeed*axis.sqrMagnitude && ovel.magnitude > vel.magnitude)
                {
                    //this is where we move
                    vel.y = y;

                    if (GroundScript.OnGround)
                    {
                        rigidScript.Rig3D.velocity = vel*.9f;//NOW WITH 10 PERCENT TIGHTER TURNS
                    }
                }
                else if (Vector3.Dot(axis, vel) > MoveSpeed)
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
                    if(whoosh != null)
                    {
                        whoosh.pitch = Random.Range(0.8f, 1.2f); // randomizes pitch   
                        whoosh.Play(); // plays whoosh sound when punch 
                    }
                }
                idealPoint = new Vector3(0, 0, -.065f);
            }

            
            
            transform.localPosition = transform.localPosition + Vector3.ClampMagnitude(idealPoint - transform.localPosition, maxSpeed);

            if (cont.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip) && Physics.Raycast(transform.position - (transform.forward - transform.up).normalized * .1f, transform.forward - transform.up, maxDist))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).CompareTag("Projection"))
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).CompareTag("Projection"))
                    {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
                if (cont.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x > .01 && Physics.Raycast(transform.position-(transform.forward - transform.up).normalized*.1f, transform.forward - transform.up, out info, .4f))
                {
                if (info.transform.tag == "playButton")
                {
                    Scene currentScene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene((currentScene.buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
                }
                if(info.transform.tag == "Enemy")
                {
                    Destroy(info.transform.gameObject);
                }
                if(info.transform.tag == "Missile")
                {
                    Destroy(info.transform.gameObject);
                }
                if (info.transform.tag != "Boost" && info.transform.tag != "enemy" && info.transform.name != "Player" && canLaunch)
                {              
                    canLaunch = false;
                    cont.TriggerHapticPulse(3000, Valve.VR.EVRButtonId.k_EButton_Axis4);
                    Vector3 vel = rigidScript.Rig3D.velocity;
                    vel.y = 0;
                    if ((vel + ((transform.forward - transform.up).normalized * -.3f / Time.deltaTime * .55f)).magnitude < vel.magnitude)
                    {
                        vel += ((transform.forward - transform.up).normalized * -.3f / Time.deltaTime * .55f) * 2;
                    }
                    else
                    {
                        vel += ((transform.forward - transform.up).normalized * -.3f / Time.deltaTime * .55f);
                    }
                    
                    punchTimer = 0;
                    rigidScript.Rig3D.velocity = vel;
                    if (info.collider.gameObject.tag == "Pillar")
                    {
                        rigidScript.Rig3D.AddForce(((transform.forward - transform.up).normalized * -.3f / Time.deltaTime * 800f * 2));
                    }

                    if (punch != null)
                    {
                        punch.pitch = Random.Range(0.8f, 1.2f);
                        punch.Play();
                    }

                }
               
            }
        }
        if (speedImmune > 0)
            rigidScript.Rig3D.velocity = Vector3.ClampMagnitude(rigidScript.Rig3D.velocity, 25 * speedImmune*speedImmune);
        else
        {
            rigidScript.Rig3D.velocity = Vector3.ClampMagnitude(rigidScript.Rig3D.velocity, 25);
        }
        
        prevLocalPos = transform.localPosition;
        prevpos = transform.position;

        ////////////Wind Sounds///////////
        if (wind != null)
        {
            if (GroundScript.OnGround)
            {
                wind.volume -= 0.5f * Time.deltaTime; // fade out
            }

            else if (rigidScript.Rig3D.velocity.magnitude != 0)
            {
                wind.volume = (rigidScript.Rig3D.velocity.magnitude / 25); // wind volume depends on velocity
            }


            else
            {
                wind.volume -= 0.2f * Time.deltaTime; // fade out
            }
        }
        /////////Footstep Sounds/////////
        if (foot1 != null)
        {
            int num = Random.Range(1, 3);
            if (GroundScript.OnGround && rigidScript.Rig3D.velocity.magnitude > 1.0f && rigidScript.Rig3D.velocity.magnitude < 5.0f && timeForSteps > 50.0f) // walking speed
            {
                foot1.pitch = Random.Range(0.8f, 1.2f);
                foot1.Play();
                timeForSteps = 0;
            }

            else if (GroundScript.OnGround && rigidScript.Rig3D.velocity.magnitude >= 5.0f && timeForSteps > 25.0f) // running speed
            {
                foot1.pitch = Random.Range(0.8f, 1.2f);
                foot1.Play();
                timeForSteps = 0;
            }
            timeForSteps++;
        }
        //////////Landing Sound///////////
        if (wind != null)
        {
            if (GroundScript.OnGround == false)
            {
                wasInAir = true;
            }

            if (GroundScript.OnGround == true && wasInAir == true)
            {
                landing.Play();
                wasInAir = false;
            }
        }

    }
    


}

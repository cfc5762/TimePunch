using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {
    public static bool OnGround;
    Vector3 fwdChk;
    Vector3 bckChk;
    Vector3 lftChk;
    Vector3 rgtChk;
	// Use this for initialization
	void Start () {
        fwdChk = new Vector3(.25f, 1, 0);
        bckChk = new Vector3(-.25f, 1, 0);
        lftChk = new Vector3(0, 1, .25f);
        rgtChk = new Vector3(0, 1, -.25f);
        OnGround = false;
	}
    private void FixedUpdate()
    {
        print(OnGround);
       
        if (Physics.Raycast(origin:transform.position, direction:-1*transform.up,maxDistance: 1.1f))
        {
            
            OnGround = true;
        }
        else if (Physics.Raycast(transform.position+fwdChk, -1 * transform.up, 1.1f))
        {

            OnGround = true;
        }
        else if (Physics.Raycast(transform.position+rgtChk, -1 * transform.up, 1.1f))
        {

            OnGround = true;
        }
        else if (Physics.Raycast(transform.position+bckChk, -1 * transform.up, 1.1f))
        {

            OnGround = true;
        }
        else if (Physics.Raycast(transform.position+lftChk, -1 * transform.up, 1.1f))
        {

            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
        transform.position = new Vector3(Head.Headpos.x, transform.position.y, Head.Headpos.z);
    }
   
   
  
}

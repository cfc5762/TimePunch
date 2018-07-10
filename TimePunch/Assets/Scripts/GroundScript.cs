using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {
    public static bool OnGround;
    bool setback;
    RaycastHit fwd;
    RaycastHit lft;
    RaycastHit rgt;
    RaycastHit bck;
    RaycastHit mid;

    public static Vector3 Forward;
    public static Vector3 Right;
    public static Vector3 Left;
    public static Vector3 Back;
    Vector3 fwdChk;
    Vector3 bckChk;
    Vector3 lftChk;
    Vector3 rgtChk;
	// Use this for initialization
	void Start () {
        setback = false;
        fwd = new RaycastHit();
        lft = new RaycastHit();
        rgt = new RaycastHit();
        bck = new RaycastHit();
        mid = new RaycastHit();
        fwdChk = new Vector3(.25f, 1, 0);
        bckChk = new Vector3(-.25f, 1, 0);
        lftChk = new Vector3(0, 1, .25f);
        rgtChk = new Vector3(0, 1, -.25f);
        OnGround = false;
	}
    private void FixedUpdate()
    {

       
        if (Physics.Raycast(origin: transform.position+new Vector3(0,1,0), direction: -1 * transform.up, maxDistance: 1.05f, hitInfo: out mid))
        {
            
            OnGround = true;
        }
        else
        {
            rigidScript.Rig3D.useGravity = true;
            OnGround = false;
            mid = new RaycastHit();
        }
        if (Physics.Raycast(origin: transform.position + new Vector3(0, 1, 0) + Head.lookDir*.25f, direction: -1 * transform.up, maxDistance: 1.01f, hitInfo: out fwd))
        {
           
            Forward = fwd.point-mid.point;
            
        }
        else
        {
            fwd = new RaycastHit();
        }
        if (Physics.Raycast(origin: transform.position + new Vector3(0, 1, 0) + Head.rightDir*.25f, direction: -1 * transform.up, maxDistance: 1.01f, hitInfo: out rgt))
        {
            Right = rgt.point - mid.point;
            
        }
        else
        {
            rgt = new RaycastHit();
        }
        if (Physics.Raycast(origin: transform.position + new Vector3(0, 1, 0) - Head.lookDir*.25f, direction: -1 * transform.up, maxDistance: 1.01f, hitInfo: out bck))
        {
            Back = bck.point - mid.point;
            
        }
        else
        {
            bck = new RaycastHit();
        }
        if (Physics.Raycast(origin: transform.position + new Vector3(0, 1, 0) - Head.rightDir*.25f, direction: -1 * transform.up, maxDistance: 1.01f, hitInfo: out lft))
        {
            Left = lft.point - mid.point;
            
        }
        else
        {
            lft = new RaycastHit(); 
        }
       
        transform.position = new Vector3(Head.Headpos.x, transform.position.y, Head.Headpos.z);
    }

    private void OnDrawGizmos()
    {
        if (OnGround)
        {
            Gizmos.DrawLine(mid.point, fwd.point);
            Gizmos.DrawLine(mid.point, rgt.point);
            Gizmos.DrawLine(mid.point, lft.point);
            Gizmos.DrawLine(mid.point, bck.point);
        }
    }


}

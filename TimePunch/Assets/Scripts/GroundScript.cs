using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {
    public static bool OnGround;
	// Use this for initialization
	void Start () {
        OnGround = false;
	}
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -1 * transform.up, 1.6f))
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
    }
   
   
  
}

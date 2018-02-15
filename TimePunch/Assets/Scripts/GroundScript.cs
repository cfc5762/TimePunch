using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {
    public static bool OnGround;
	// Use this for initialization
	void Start () {
        OnGround = false;
	}
	
	// Update is called once per frame
	void OnCollisionStay (Collision C) {
		
	}
}

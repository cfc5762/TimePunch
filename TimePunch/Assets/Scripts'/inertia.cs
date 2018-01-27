using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inertia : MonoBehaviour {
    
    Rigidbody Rig3D;
    Vector3 prevpos;
	// Use this for initialization
	void Start () {
        Rig3D = GetComponent<Rigidbody>();
        prevpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Fist.LeftHit.sqrMagnitude != 0)
        {
            print("good");
            Rig3D.AddForce(Fist.LeftHit);
            Fist.LeftHit = Vector3.zero;
        }
	}
    
}

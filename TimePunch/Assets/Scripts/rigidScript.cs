using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidScript : MonoBehaviour {
    public static Rigidbody Rig3D;
	// Use this for initialization

    void Start () {
        Rig3D = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}

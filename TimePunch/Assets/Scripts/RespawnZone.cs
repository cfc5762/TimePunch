using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour {
    public static Vector3 destination;
	// Use this for initialization
	void Start () {
        destination = GameObject.FindGameObjectWithTag("Respawn").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnTriggerEnter(Collider col)
    {
        rigidScript.Rig3D.position = destination;
        Timer.Restart();
    }
}

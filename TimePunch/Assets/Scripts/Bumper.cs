using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public float knockBackPower;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Bumper has collided");

        //Check for player collision
        if (other.transform.root.gameObject.tag=="Player")
        {
            Vector3 knockBack = (other.transform.position - gameObject.transform.position);     //calculate knockback
            knockBack.Normalize();
            knockBack *= knockBackPower;
            rigidScript.Rig3D.velocity = knockBack;        //add knockback
            Debug.Log("Bumper has collided with player");
        }
    }
}

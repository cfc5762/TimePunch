using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementY : MonoBehaviour {
    private float speed = 6.0f;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.y > 170)
        {
            speed = -speed;
        }

        else if(transform.position.y < 128.5)
        {
            speed = -speed;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}

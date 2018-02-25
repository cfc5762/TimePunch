using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementY : MonoBehaviour {
    public float speed = 6.0f;
    public float top;
    public float bottom;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.y > top)
        {
            speed = -speed;
        }

        else if(transform.position.y < bottom)
        {
            speed = -speed;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}

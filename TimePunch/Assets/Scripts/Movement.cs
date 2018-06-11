using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed;
    public float bound1;
    public float bound2;
    public bool horizontal;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(horizontal == false)
        {
            if (transform.position.y > bound1)
            {
                speed = -speed;
            }

            else if (transform.position.y < bound2)
            {
                speed = -speed;
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        else
        {
            if (transform.position.z > bound1)
            {
                speed = -speed;
            }

            else if (transform.position.z < bound2)
            {
                speed = -speed;
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }       
	}
}

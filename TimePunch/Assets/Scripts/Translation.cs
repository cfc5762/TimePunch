using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour {
    public float distanceToTravel;
    [Range(-0.1f,0.1f)]
    public float speedPerFrame;
    public bool horizontal;

    public float distanceRemaining;
	// Use this for initialization
	void Start () {
        distanceRemaining  = distanceToTravel;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newMovement;

        if (horizontal)
        {
            newMovement = new Vector3(speedPerFrame, 0f, 0f);
        }
        else
        {
            newMovement = new Vector3(0f, speedPerFrame, 0f);
        }

        transform.position = transform.position + newMovement;
        distanceRemaining -= Mathf.Abs(speedPerFrame);

        if (distanceRemaining <= 0)
        {
            speedPerFrame *= -1;
            distanceRemaining = Mathf.Abs(distanceToTravel);
        }
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBird : Mover {
    //public GameObject Followpoint;
    int nodepos;
    System.Random R;
    public Vector3 seekLocation;
    public List<GameObject> Nodes;
    public Vector3 NextSpot()
    {
        return transform.position - transform.forward * (base.radius * 20);
    }
    public Vector3 NextSpot(Mover Next)
    {
        return transform.position - transform.forward * (base.radius * 20 + Next.radius);
    }
    protected override void CalcSteering()
    {
        float t = Vector3.Distance(transform.position, seekLocation);
        if (t < 10)
        {
            nextNode();
        }
        ApplyForce(Seek(seekLocation));
    }
    void nextNode()
    {
        nodepos++;
       nodepos = nodepos % Nodes.Count;
        seekLocation = Nodes[nodepos].transform.position;
        seekLocation += new Vector3((float)R.NextDouble() * 4f - 2f, (float)R.NextDouble() * 4f - 2f, (float)R.NextDouble() * 4f - 2f);
    }
    // Use this for initialization
    void Start () {
        R = new System.Random();
        nodepos = 0;
        seekLocation = Nodes[nodepos].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Followpoint.transform.position = NextSpot();
    }
}

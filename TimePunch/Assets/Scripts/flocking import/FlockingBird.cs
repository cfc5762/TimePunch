using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingBird : Mover {
    public List<Transform> Path = new List<Transform>();
    int currPos = 0;
    public GameObject G;
    Vector3 TempAv;
    Vector3 farthestGoon;
    Vector3 GoonAvg;
    float GoonRad;
    public string Allegiance;
    public LeaderBird Leader;
    List<Mover> Homies = new List<Mover>();
    List<Mover> Goons = new List<Mover>();
    protected override void CalcSteering()
    {
        if (Allegiance == "group1") { 
        ApplyForce(Flock(ArriveVel(Leader.transform.position, 20f, 0f), Homies, 20f, 43f, 1f, 0f));

        }
        else if (Allegiance == "group2")
        {
            ApplyForce(Flock(ArriveVel(NextSpot, 5f, 0f), Homies, 5f, 2f, 1f, 1.1f));
        }

        if (G != null)
        {
            G.transform.position = NextSpot;
        }
    }
    public Vector3 NextSpot
    {
        get
        {
            return GoonAvg + (GoonAvg-Leader.NextSpot(this)).normalized*GoonRad*2;
        }
    }
    // Use this for initialization
    void Start () {
        TempAv = Vector3.zero;
        GoonAvg = Vector3.zero;
        GoonRad = 0f;
       
        if (Allegiance == "group2")
        {
            GameObject[] guys = GameObject.FindGameObjectsWithTag("group1");
            foreach (var item in guys)
            {
                if (item.GetComponent<Mover>() != null)
                {
                    Goons.Add(item.GetComponent<Mover>());
                }
                else
                {
                    print("unity hates inheritance");
                }
            }
        }
        GameObject[] buds = GameObject.FindGameObjectsWithTag(Allegiance);
        foreach (var item in buds)
        {
            if (item.GetComponent<Mover>() != null)
            {
                Homies.Add(item.GetComponent<Mover>());
            }
            else
            {
                print("unity hates inheritance");
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        farthestGoon = GoonAvg;
        TempAv = Vector3.zero;
        foreach (var item in Goons)
        {
            if ((GoonAvg-item.transform.position).sqrMagnitude>(GoonAvg-farthestGoon).sqrMagnitude)
            {
                farthestGoon = item.transform.position;//find farthest goon from last frames average
            }
            TempAv += item.transform.position;
        }

        GoonAvg = TempAv / Goons.Count;
        GoonRad = (GoonAvg - farthestGoon).magnitude;

	}
}

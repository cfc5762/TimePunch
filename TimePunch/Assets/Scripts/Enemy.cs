using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int maxMissiles;
    private int missileNum;

    

	// Use this for initialization
	void Start () {
        missileNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ShootMissile()
    {
        //
        missileNum++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.name == "Player")
        {
            ShootMissile();
        }
    }
}

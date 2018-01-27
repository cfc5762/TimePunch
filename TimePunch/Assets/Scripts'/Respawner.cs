using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {
    private Vector3 respawnPoint;
    private GameObject[] platforms;

	// Use this for initialization
	void Start () {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject p in platforms)
            if (this.transform.position.y < p.transform.position.y - 40f)
                this.transform.position = respawnPoint;
	}
}

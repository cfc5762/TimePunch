﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    private float rpm = 10.0f;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(0, (float)(6.0 * rpm * Time.deltaTime), 0);
	}
}

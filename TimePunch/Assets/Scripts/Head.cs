using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {
    public static Vector3 Headpos;
    public static Vector3 lookDir;
    public static Vector3 rightDir;
    Vector3 temp;
	// Use this for initialization
	void Start () {
        rightDir = Vector3.zero;
        lookDir = Vector3.zero;
        temp = Vector3.zero;
        Headpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Headpos = transform.position;
        temp = transform.forward;
        temp.y = 0;
        lookDir = temp.normalized;
        temp = transform.right;
        temp.y = 0;
        rightDir = temp.normalized;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {
    public static bool OnGround;
	// Use this for initialization
	void Start () {
        OnGround = false;
	}

    // Update is called once per frame
    void OnCollisionEnter(Collision C)
    {
        if (rigidScript.Rig3D.velocity.y > -0.5 && rigidScript.Rig3D.velocity.y < 0.5)
            OnGround = true;
    }
    void OnCollisionStay(Collision C)
    {
        if (rigidScript.Rig3D.velocity.y > -0.5&& rigidScript.Rig3D.velocity.y < 0.5)
            OnGround = true;
    }
    void OnCollisionExit(Collision C)
    {
        OnGround = false;
    }
}

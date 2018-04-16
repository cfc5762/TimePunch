using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {
    public int punchMult;
    public int immuneFrames;
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {

        GameObject oth = other.gameObject;
        while (oth.transform.parent != null)
        {
            oth = oth.transform.parent.gameObject;
        }
        if (oth.tag == "Player")
        {
          
            Fist.speedImmune = immuneFrames;
           
            
            Vector3 vel = rigidScript.Rig3D.velocity;
            
            
                vel += ((transform.position - target.position).normalized * -1 / Time.deltaTime * .55f)*punchMult;
            rigidScript.Rig3D.velocity = vel;
            
        }
    }
}

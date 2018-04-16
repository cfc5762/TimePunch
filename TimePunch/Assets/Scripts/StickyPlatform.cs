using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour {
    List<Transform> objs = new List<Transform>();//objects on the platform
                                                 // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}
    private void OnCollisionStay(Collision collision)
    {


        Transform Obj = collision.transform;
        while (Obj.parent != null)
        {
            if (Obj.tag == "Player")
            {
                break;
            }
            Obj = Obj.parent;
        }
        if (!objs.Contains(Obj))
            objs.Add(Obj);
        Obj.parent = transform;
    }
    private void OnCollisionExit(Collision collision)
    {
        Transform Obj = collision.transform;
        while (Obj.parent != null)
        {
            if (objs.Contains(Obj))
            {
                objs.Remove(Obj);
                Obj.parent = null;
            }
            else
            {
                Obj = Obj.parent;
            }

        }
        
    }
}

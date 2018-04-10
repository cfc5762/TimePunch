using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public List<Camera> Cams;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            foreach (var item in Cams)
            {
                item.enabled = !item.enabled;
            }
        }
    }
}

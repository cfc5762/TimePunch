using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBarAnimation : MonoBehaviour
{
    public float speed;
    public float count;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        count = Mathf.PingPong(Time.time * speed, 1.0f);
        if(gameObject.tag == "2")
        {
            if (count > .1f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }
        
        if(gameObject.tag == "3")
        {
            if (count > .2f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }

        if (gameObject.tag == "4")
        {
            if (count > .3f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }

        if (gameObject.tag == "5")
        {
            if (count > .4f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }

        if (gameObject.tag == "6")
        {
            if (count > .5f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }

        if (gameObject.tag == "7")
        {
            if (count > .6f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }

        if (gameObject.tag == "8")
        {
            if (count > .7f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }

        if (gameObject.tag == "9")
        {
            if (count > .8f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }

        if (gameObject.tag == "10")
        {
            if (count > .9f)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    public float speed;
    public float color;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        color = Mathf.PingPong(Time.time * speed, 1.0f);
        this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, color);





        /*Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;
        
        
        
        //Color baseColor = Color.green;

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
       /* Color myColor = new Color();
        ColorUtility.TryParseHtmlString("#00FF17", out myColor);*/
       // mat.SetColor("_EmissionColor", finalColor);*/
	}
}

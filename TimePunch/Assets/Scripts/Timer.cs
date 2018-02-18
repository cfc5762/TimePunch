using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    bool isDone;
    float timeSoFar;
    public Font f;
	// Use this for initialization
	void Start () {
        timeSoFar = 0f;
        isDone = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(!isDone)
            timeSoFar += Time.deltaTime;

	}

    //
    public void finishLevel()
    {
        isDone = true;
    }

    void OnGUI()
    {
        GUI.skin.font = f;
        
        float minutes = Mathf.Floor(timeSoFar / 60);
        float seconds = timeSoFar % 60;
        
        if (Mathf.RoundToInt(seconds)< 10)
            GUI.Label(new Rect(Screen.width * 0.45f, 30, 250, 100), minutes + ":0" + Mathf.RoundToInt(seconds));
        else
            GUI.Label(new Rect(Screen.width*0.45f, 30, 250, 100), minutes + ":" + Mathf.RoundToInt(seconds));

    }
}

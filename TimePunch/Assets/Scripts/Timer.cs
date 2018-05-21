using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public static float timeSoFar;                //current time in frames
    private bool isDone;
    private float minutes;
    private float seconds;
    private string displayedTime;   //current time in the level as displayed as a string
    public Font timerFont;          
    GameObject inGameTimer;
    TextMesh textMesh;
    // Use this for initialization
	void Start () {
        timeSoFar = 0f;
        isDone = false;
        inGameTimer = new GameObject();
        inGameTimer.transform.SetParent(gameObject.transform);
        textMesh = inGameTimer.AddComponent<TextMesh>();
        textMesh.fontSize = 200;
        textMesh.alignment = TextAlignment.Center;
        
        
        inGameTimer.transform.localScale = new Vector3(-0.0025f,0.0025f,0.0025f);
        inGameTimer.transform.Rotate(new Vector3(0.0f, 90.0f, -135.0f));
        
        //inGameTimer.transform.SetPositionAndRotation(Vector3.zero,);
        inGameTimer.transform.position = gameObject.transform.position;
        inGameTimer.transform.localPosition += new Vector3(0.0f, -0.01f, -0.15f);
    }

    // Update is called once per frame
    void Update () {
        if(!isDone)
            timeSoFar += Time.deltaTime;
        minutes = Mathf.Floor(timeSoFar / 60);
        seconds = timeSoFar % 60;
        if (Mathf.RoundToInt(seconds) < 10)
            displayedTime = minutes + ":0" + Mathf.RoundToInt(seconds);
        else
            displayedTime = minutes + ":" + Mathf.RoundToInt(seconds);
        textMesh.text = displayedTime;
    }

    //
    public void finishLevel()
    {
        isDone = true;
    }

    void OnGUI()
    {
        GUI.skin.font = timerFont;
        
        minutes = Mathf.Floor(timeSoFar / 60);
        seconds = timeSoFar % 60;
        
        if (Mathf.RoundToInt(seconds)< 10)
            GUI.Label(new Rect(Screen.width * 0.45f, 30, 250, 100), minutes + ":0" + Mathf.RoundToInt(seconds));
        else
            GUI.Label(new Rect(Screen.width*0.45f, 30, 250, 100), minutes + ":" + Mathf.RoundToInt(seconds));

    }

    public static void Restart()
    {
        timeSoFar = 0.0f;
    }
}

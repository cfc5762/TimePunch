using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public static float timeSoFar;                //current time in frames
    private bool isDone;
    private float minutes;
    private float seconds;
    private string displayedTime;   //current time in the level as displayed as a string

    private Vector3 midpoint;
    private float radius;

    private Vector3 playerHeadLocation;

    private Vector3 newLocalPosition;

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

        radius = 0.1f;
        
        inGameTimer.transform.localScale = new Vector3(0.0025f,-0.0025f,0.0025f);
        //inGameTimer.transform.Rotate(new Vector3(0.0f, 90.0f, -135.0f));
        
        //inGameTimer.transform.SetPositionAndRotation(Vector3.zero,);
        inGameTimer.transform.position = gameObject.transform.position;
        //inGameTimer.transform.localPosition += new Vector3(0.0f, 0.01f, -0.1f);
    }

    // Update is called once per frame
    void Update () {
        playerHeadLocation = GameObject.Find("FollowHead").transform.position;
        midpoint = transform.parent.position;

        float euclidianNorm = Mathf.Sqrt(Mathf.Pow(playerHeadLocation.x - midpoint.x, 2) + Mathf.Pow(playerHeadLocation.y - midpoint.y, 2) +Mathf.Pow(playerHeadLocation.z-midpoint.z,2));
        //newLocalPosition.x = midpoint.x + radius * ((playerHeadLocation.x - midpoint.x) / euclidianNorm);
        //newLocalPosition.z = midpoint.y + radius * ((playerHeadLocation.y - midpoint.y) / euclidianNorm);
        //inGameTimer.transform.localPosition = newLocalPosition;
        newLocalPosition =  radius * ((playerHeadLocation - midpoint)/euclidianNorm);
        //newLocalPosition = (midpoint - playerHeadLocation) * radius;
        //newLocalPosition.y = 0;
        inGameTimer.transform.localPosition = newLocalPosition;

        if (!isDone)
            timeSoFar += Time.deltaTime;
        minutes = Mathf.Floor(timeSoFar / 60);
        seconds = timeSoFar % 60;
        if (Mathf.RoundToInt(seconds) < 10)
            displayedTime = minutes + ":0" + Mathf.RoundToInt(seconds);
        else
            displayedTime = minutes + ":" + Mathf.RoundToInt(seconds);
        textMesh.text = displayedTime;

        orient();
    }

    private void orient()
    {
        Vector3 lookDirection = (playerHeadLocation - inGameTimer.transform.position);
        inGameTimer.transform.rotation = Quaternion.LookRotation(lookDirection);

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

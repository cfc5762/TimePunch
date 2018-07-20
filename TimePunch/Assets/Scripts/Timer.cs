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

    public int currentNode;

    public int numNodes;
    public Vector3[] nodes;
    public GameObject[] nodesGO;
    private GameObject nodeGO;

    private Vector3 playerHeadLocation;

    private Vector3 newLocalPosition;

    public Font timerFont;          
    GameObject inGameTimer;
    TextMesh textMesh;
    // Use this for initialization
	void Start () {
        nodes = new Vector3[numNodes];
        nodesGO = new GameObject[numNodes];

        nodeGO = new GameObject();
        nodeGO.AddComponent<Transform>();

        timeSoFar = 0f;
        isDone = false;
        inGameTimer = new GameObject();
        inGameTimer.transform.SetParent(gameObject.transform);
        textMesh = inGameTimer.AddComponent<TextMesh>();
        textMesh.fontSize = 200;
        textMesh.alignment = TextAlignment.Center;

        radius = 0.015f;
        
        inGameTimer.transform.localScale = new Vector3(0.00025f,-0.00025f,0.00025f);
        //inGameTimer.transform.Rotate(new Vector3(0.0f, 90.0f, -135.0f));
        
        //inGameTimer.transform.SetPositionAndRotation(Vector3.zero,);
        inGameTimer.transform.position = gameObject.transform.position;
        //inGameTimer.transform.localPosition += new Vector3(0.0f, 0.01f, -0.1f);
        createNodes();
    }

    // Update is called once per frame
    void Update () {
        playerHeadLocation = GameObject.Find("FollowHead").transform.position;
        midpoint = transform.position;

        currentNode = getClosestNode();
        
        inGameTimer.transform.position = nodesGO[currentNode].transform.position;
        //float euclidianNorm = Mathf.Sqrt(Mathf.Pow(playerHeadLocation.x - midpoint.x, 2) + Mathf.Pow(playerHeadLocation.y - midpoint.y, 2) +Mathf.Pow(playerHeadLocation.z-midpoint.z,2));
        //newLocalPosition.x = midpoint.x + radius * ((playerHeadLocation.x - midpoint.x) / euclidianNorm);
        //newLocalPosition.z = midpoint.y + radius * ((playerHeadLocation.y - midpoint.y) / euclidianNorm);
        //inGameTimer.transform.localPosition = newLocalPosition;
        //newLocalPosition =  radius * ((midpoint - playerHeadLocation));
        
        //newLocalPosition = (midpoint - playerHeadLocation) * radius;
        //newLocalPosition.y = 0;
        //inGameTimer.transform.localPosition = newLocalPosition;

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

    public int getClosestNode()
    {
        int closestNode = -1;
        float minDistance = 100f;
        for (int i = 0; i < nodes.Length; i++)
        {
            float currentDistance = Vector3.Distance(playerHeadLocation, transform.TransformPoint(transform.localPosition + nodes[i]));
            if (currentDistance<minDistance)
            {
                minDistance = currentDistance;
                closestNode = i;
            }
        }

        return closestNode;
    }

    void createNodes()
    {
        
        for (int i = 0; i < numNodes; i++)
        {
            //Radius*VectorHelper.AngleToUnit(Mathf.Deg2Rad*((360 * i) / numBuzzPathNodes));
            Vector3 pos= radius * AngleToUnit(Mathf.Deg2Rad * ((360 * i) / numNodes));
            Debug.Log(pos);
            pos.x = -pos.x;
            nodes[i] = pos;

            nodesGO[i] = Instantiate(nodeGO, transform.position, Quaternion.identity);
            nodesGO[i].transform.parent = transform;
            nodesGO[i].transform.localPosition = nodes[i];
        }
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

    public static Vector3 AngleToUnit(float radians)
    {
        // hooray trigonometry!
        return new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
    }
}

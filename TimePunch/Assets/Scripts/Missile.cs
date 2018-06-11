using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float maxSpeed;
    Vector3 playerPos;
    Vector3 prevPlayerPos;

    Vector3 acceleration;
    Vector3 velocity;


    // Use this for initialization
    void Start () {
        playerPos = rigidScript.Rig3D.position;

    }
	
	// Update is called once per frame
	void Update () {
        prevPlayerPos = playerPos;
        playerPos = rigidScript.Rig3D.position;

        if(Vector3.Distance(prevPlayerPos,playerPos)>=5)
        {
            Destroy(this.gameObject);
        }

        //seek player
        Vector3 toTarget = playerPos - transform.position;
        Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
        Vector3 steeringForce = desiredVelocity - velocity;

        ApplyForce(steeringForce);


        velocity += acceleration;
        velocity = VectorHelper.Clamp(velocity, maxSpeed);
        transform.position += velocity;

        gameObject.transform.rotation = Quaternion.LookRotation(velocity);


    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fist")
        {
            Debug.Log("Missile has been punched");
            Destroy(this.gameObject);
        }
        else if (other.transform.root.gameObject.name == "Player")
        {
            Timer.Restart();
            rigidScript.Rig3D.position = RespawnZone.destination;
            Destroy(this.gameObject);

        }
    }
}

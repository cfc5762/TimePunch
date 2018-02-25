using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //checks if the things entering the trigger is the player or a sub object of that player (In this case, the BodyCollider object is what we're looking to use.
        //also makes sure the player can not use the colliders on their fist to get the Goal
        if (other.transform.root.gameObject.name=="Player"&&other.gameObject.tag!="Fist")
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene((currentScene.buildIndex + 1)%SceneManager.sceneCountInBuildSettings);
            

        }
    }
}

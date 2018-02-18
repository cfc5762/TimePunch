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

            switch (currentScene.name)
            {
                case "Level1":
                    SceneManager.LoadScene("Scenes/Level2", LoadSceneMode.Single);
                    break;
                case "Level2":
                    SceneManager.LoadScene("Scenes/arena3", LoadSceneMode.Single);
                    break;
                case "arena3":
                    SceneManager.LoadScene("Scenes/Zone4", LoadSceneMode.Single);
                    break;
                case "Zone4":
                    SceneManager.LoadScene("Scenes/Level1");
                    break;
                default:
                    SceneManager.LoadScene("Scenes/Level1", LoadSceneMode.Single);
                    break;                   
            }

        }
    }
}

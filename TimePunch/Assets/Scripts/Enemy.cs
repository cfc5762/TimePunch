using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int maxMissiles;
    //public int missileTimer;
    public float aggroDistance;

    private int missileNum;
    private GameObject[] missiles;

    UnityEngine.Object missilePrefab;

    // Use this for initialization
    void Start () {        
        missiles = new GameObject[maxMissiles];
        missileNum = 0;
        missilePrefab = AssetDatabase.LoadAssetAtPath("Assets/Scenes/Prefabs/Missile.prefab", typeof(GameObject));
    }

    // Update is called once per frame
    void Update () {
		if(Vector3.Distance(transform.position,rigidScript.Rig3D.position)<=aggroDistance&&missileNum<maxMissiles)
            ShootMissile();

        missileNum = 0;
        for (int i = 0; i < maxMissiles; i++)
        {
            if (missiles[i] != null)
                missileNum++;

        }
	}

    void ShootMissile()
    {
        Debug.Log("in ShootMissile");
        //
        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity) as GameObject;
        missiles[missileNum] = missile;
        missileNum++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fist")
        {
            Debug.Log("Enemy has been has been punched");
            Destroy(this.gameObject);
        }
        else if (other.transform.root.gameObject.name == "Player")
        {
            Debug.Log("Enemy has hit the player!");
            Destroy(this.gameObject);
        }
    }
}

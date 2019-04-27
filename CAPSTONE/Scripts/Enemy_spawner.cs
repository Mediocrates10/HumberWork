using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_spawner : MonoBehaviour {

    public GameObject enemyPrefab;
    bool canSpawn = true;

    public List<Transform> spawnLoc;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < spawnLoc.Count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnLoc[i].position, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update (){
		
	}

    void OnTriggerEnter(Collider coll) {

        if (canSpawn == true && coll.gameObject.tag == "Player") {
            SpawnIt();
            canSpawn = false;
        }
    }


    void SpawnIt() {
        //Spawn an emeny for every spwan location
        for (int i = 0; i < spawnLoc.Count; i++) {
            GameObject enemy = Instantiate(enemyPrefab, spawnLoc[i].position, Quaternion.identity);
        }
    }
}

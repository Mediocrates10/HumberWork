using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_rock : MonoBehaviour {

    Dunga target;
    public bool isHeld = false;

	// Use this for initialization
	void Start () {
        Invoke("DestroyRock", 7);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.tag == "Player")
        {
            target.hasRock = true;
        }
    }

    void DestroyRock() 
    {
        Destroy(gameObject);
    }
}

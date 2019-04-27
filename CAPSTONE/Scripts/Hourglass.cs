using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : MonoBehaviour {

    public GameObject broken;



    void OnCollisionEnter(Collision coll)
    {
        // when its hit the ground replace model with boeken one
        // Then set time scale to half
        if (coll.gameObject.tag == "Ground") {

            Instantiate(broken, transform.position, transform.rotation);
            Destroy(gameObject);
        }
         

    }

   
}

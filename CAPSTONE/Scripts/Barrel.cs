using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

    int critCheck; 
    int damage;

    public GameObject broken;


    // Use this for initialization
    void Start () {
        
        critCheck = Random.Range(1, 11);
        damage = Random.Range(9, 12);
    }

    // Update is called once per frame
    void Update () {

        
    }

    void OnCollisionEnter(Collision coll) {
        
        if(coll.gameObject.tag == "Enemy") {

            //Instantiate(broken, transform.position, transform.rotation);
            //Destroy(gameObject);
            //Destroy(broken, 2f);

            if (coll.collider.gameObject.GetComponent<Barrier_Buff>() != null)
            {
                Debug.Log("Hit shield");
                Destroy(gameObject);
            }

                if (coll.collider.gameObject.GetComponent<EnemyMarauder>() != null)  {
                coll.collider.gameObject.GetComponent<EnemyMarauder>().Damage(damage, critCheck);
                Instantiate(broken, transform.position, transform.rotation);
                Destroy(gameObject);
                Destroy(GameObject.Find("CrackedBarrel (1)(Clone)"), 2);
                
            }

            else if (coll.collider.gameObject.GetComponent<EnemyArcher>() != null) {
                coll.collider.gameObject.GetComponent<EnemyArcher>().Damage(damage, critCheck);
                Instantiate(broken, transform.position, transform.rotation);
                Destroy(gameObject);
                Destroy(GameObject.Find("CrackedBarrel (1)(Clone)"), 2);

            }

            else if (coll.collider.gameObject.GetComponent<Boss_1>() != null) {
                coll.collider.gameObject.GetComponent<Boss_1>().Damage(damage, critCheck);
                Instantiate(broken, transform.position, transform.rotation);
                Destroy(gameObject);
                Destroy(GameObject.Find("CrackedBarrel (1)(Clone)"), 2);
            }

            
        }
    }
}

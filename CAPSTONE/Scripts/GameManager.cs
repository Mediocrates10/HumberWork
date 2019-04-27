using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class GameManager : MonoBehaviour {

    public int eCount;
    ///public bool stageClear = false;
    public GameObject[] enemies;

    public Animator ani;

    public PlayableDirector timeline;

    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        eCount = enemies.Length;

        ani.SetBool("IsOpen", false);
    }
	
	// Update is called once per frame
	void Update () {
       
        //start timeline 
        if (eCount <= 0) {
            nextStage();
        }
        //end timeline
        else if(eCount > 0) {
            Reset();
        }



        if(GetComponent<Dunga>().health == 0)
        {
            ani.SetBool("IsOpen", true);
        }
       
    }

    void nextStage() {
        timeline.Play();
        // switch to next stage

        // spawn more enemies

        // count enemies again
        countEnemies();
    }

    void Reset() {
        timeline.Stop();
    }


    void countEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        eCount = enemies.Length;
          
    }
}

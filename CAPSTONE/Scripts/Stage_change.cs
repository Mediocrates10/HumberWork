using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Stage_change : MonoBehaviour
{
    public PlayableDirector timeLine;

    // Use this for initialization
    void Start() {
        timeLine = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update() {

    }


    void OnTriggerEnter(Collider coll) {
        if(coll.gameObject.tag  == "Player")
        {
            timeLine.Play();
            Debug.Log("this works");
        }
    }
}

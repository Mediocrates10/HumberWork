using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bak : MonoBehaviour {

    public GameObject currentWaypoint;
    public float moveSpeed;
    private float minDist = 2;
    public Animator spriteAnimator;

	// Use this for initialization
	void Start () {
        currentWaypoint = GameObject.Find("BakWP1");
        moveSpeed = 2.5f;
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) >= minDist)
        {
            MoveTowards();
            spriteAnimator.Play("BakWalk");
        }

        else
        {
            spriteAnimator.Play("BakIdle");
        }
    }


    private void MoveTowards()
    {
        transform.position += transform.right * 1 * moveSpeed * Time.deltaTime;
    }
}

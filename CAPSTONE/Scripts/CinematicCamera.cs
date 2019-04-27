using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour {

    public Transform start;
    public Transform end;

    public float speed = 14f;

    private float startTime;
    private float journeyLength;

	// Use this for initialization
	void Start () {
        startTime = Time.time;

        journeyLength = Vector3.Distance(start.position, end.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(start.position, end.position, fracJourney);

    }
}

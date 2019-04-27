using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barkeep : MonoBehaviour {

    public Animator spriteAnimator;
    private AudioSource audioPlayer;
    private AudioClip yoohooAudio;

	// Use this for initialization
	void Start () {
        audioPlayer = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Yoohoo()
    {
        spriteAnimator.Play("Yoohoo");
        audioPlayer.Play(0);
    }

}

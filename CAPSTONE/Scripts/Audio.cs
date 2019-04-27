using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour {

    private AudioSource audioSrc;
    private AudioSource audioSrc1;
    public Slider slider;
    public Slider slider1;

	// Use this for initialization
	void Start () {
        audioSrc = GameObject.Find("DialogueManager").GetComponent<AudioSource>();
        audioSrc1 = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        audioSrc.volume = slider.value;
        audioSrc1.volume = slider1.value;
    }
}

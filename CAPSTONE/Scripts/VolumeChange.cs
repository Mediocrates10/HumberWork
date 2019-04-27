using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChange : MonoBehaviour {

    private AudioSource audioSrc;
    private float m_volume = 1f;


	// Use this for initialization
	void Start ()
    {
        audioSrc = GameObject.Find("GameManager").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        audioSrc.volume = m_volume;
	}

    public void SetVolume(float vol)
    {
        m_volume = vol;
    }

    public void mute()
    {
        m_volume = 0;
    }

}

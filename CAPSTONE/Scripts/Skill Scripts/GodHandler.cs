using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodHandler : MonoBehaviour {

    public GodStats God;

    [SerializeField]
    private Canvas m_Canvas;
    private bool m_SeeCanvas;
    public GameObject pauseMenu;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (m_SeeCanvas == true)
        {
            //Pauses the game when escape is pressed 
            Time.timeScale = 0;
            //Pauses the music when escape is pressed 
            AudioListener.volume = 0;
        }

        else if (m_SeeCanvas == false && pauseMenu.activeSelf == false)
        {
            Time.timeScale = 1;
            AudioListener.volume = 1;
        }

    }

}

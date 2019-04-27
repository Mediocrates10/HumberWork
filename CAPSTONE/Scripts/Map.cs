using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject map;
    public GameObject icon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenMap()
    {
        icon.SetActive(false);
        map.SetActive(true);
    }

    public void CloseMap()
    {
        icon.SetActive(true);
        map.SetActive(false);
    }

    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

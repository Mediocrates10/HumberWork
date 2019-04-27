using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransitionScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("LoadVillage", 4f);
	}
	
    public void LoadVillage()
    {
        SceneManager.LoadScene("Village");
    }
}

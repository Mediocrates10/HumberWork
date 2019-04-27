using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Init : MonoBehaviour {

    // Use this for initialization
    void Start () {

        PauseGame gm = GameObject.Find("GameManager").GetComponent<PauseGame>();
        GodStats god = GameObject.Find("Player").GetComponent<GodStats>();
        string path = SaveSystem.gamePath;
        Debug.Log(path);

        if (File.Exists(path))
        {
            gm.LoadGame();
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            gm.SaveGame();
            SceneManager.LoadScene("MainMenu");
        }
    }

}

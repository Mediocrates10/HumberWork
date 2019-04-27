using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSelection : MonoBehaviour {

    public void OpeningScene()
    {
        Destroy(GameObject.Find("Audio Source"));
        SceneManager.LoadScene("OpeningScene");
    }

    public void GameScene()
    {
        Destroy(GameObject.Find("Audio Source"));
        SceneManager.LoadScene("TutorialScene");
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}

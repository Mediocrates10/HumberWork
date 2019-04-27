using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Cred : MonoBehaviour
{
    public Animator animName;

    void Start()
    {
        animName.Play("CreditsScroller");
        Invoke("SceneChange", 5.0f);
        Time.timeScale = 1;
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(0);
    }
}

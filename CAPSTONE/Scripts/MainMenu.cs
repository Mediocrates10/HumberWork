using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public Transform canvas;
    public Transform mainMenu;
    public Transform optionsMenu;
    public Transform credits;
   
    //Makes sure the canvases are in order in the main menu
    public void Main()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            if (mainMenu.gameObject.activeInHierarchy == false)
            {
                mainMenu.gameObject.SetActive(true);
                optionsMenu.gameObject.SetActive(false);
            }

            if (mainMenu.gameObject.activeInHierarchy == false)
            {
                mainMenu.gameObject.SetActive(true);
                credits.gameObject.SetActive(false);
            }
        }
    }

    //Loads the Game on click
    public void StartGame()
    {
        SceneManager.LoadScene("SceneSelection");
    }

    //Quits the game on click
    public void QuitButton()
    {
        Application.Quit();
    }

    //Opens up Options Canvas closes Main Menu
    public void Options(bool open)
    {
        Debug.Log(open);

        if (open)
        {
            optionsMenu.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(false);
        }

        if (!open)
        {
            optionsMenu.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }
    }

    //Opens up Credits Canvas closes Main Menu
    public void Credits()
    {
        SceneManager.LoadScene("NewCredits");
    }
}


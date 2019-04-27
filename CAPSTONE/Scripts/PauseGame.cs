using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseGame : MonoBehaviour {

    public bool combatActive = false;
    public bool tutorialSwap = false;
    public GameObject healthBarBeingDragged;
    public GameObject iconBeingDragged;
    public Transform canvas;
    public Transform pauseMenu;
    public Transform optionsMenu;
    public Transform arrowSpawn;
    public Animator ani;
    public GameObject[] Stage1Enemies = new GameObject[1];
    public GameObject[] Stage2Enemies = new GameObject[1];
    public GameObject[] Stage3Enemies = new GameObject[2];
    public GameObject[] Stage4Enemies = new GameObject[3];
    public GameObject[] Stage5Enemies = new GameObject[2];
    public GameObject[] currentStageEnemies;
    public GameObject arrowPrefab;
    public GameObject arrowCollider;

    private Scene currentScene;
    private Dialogue dialogueScript;
    private Dunga dungaScript;
    private EnemyMarauder marauderScript;
    private EnemyArcher archerScript;
    private GameObject godCanvasUI;
    private GameObject godCanvasFade;
    private GameObject dialogueParent;
    private GameObject gameOver;
    private GameObject Dunga;
    private GameObject Marauder;
    private GameObject frozenArrow;
    private TextMeshProUGUI stageText;
    private TextMeshProUGUI healthText;
    private float stageHealth;
    private int currentStage;

    public Slider slider;
    public Slider slider1;
    public AudioSource audioSrc;
    public AudioSource audioSrc1;
    public GameObject focusCircle;

    float counter = 0;

    public bool menuActive;

    private void Start()
    {
        currentStage = 1;
        SetCurrentStageEnemies();
        currentScene = SceneManager.GetActiveScene();

        
        audioSrc = GameObject.Find("DialogueManager").GetComponent<AudioSource> ();
		audioSrc1 = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource> ();
        audioSrc1.volume = 0.5f;

        godCanvasUI = GameObject.Find("HorizontalBarUI");
        godCanvasFade = GameObject.Find("BlackFade");

        if (GameObject.Find("Pause") != null)
        {
        canvas = GameObject.Find("Pause").transform;
        }

        if (GameObject.Find("StageText") != null)
        {
            stageText = GameObject.Find("StageText").GetComponent<TextMeshProUGUI>();
            healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        }

        if (GameObject.Find("DialogueBox") != null)
        {
        dialogueParent = GameObject.Find("DialogueBox");
        dialogueScript = GameObject.Find("DialogueManager").GetComponent<Dialogue>();
        dialogueParent.SetActive(false);
        }

        if (GameObject.Find("Character") != null)
        {
            dungaScript = GameObject.Find("Character").GetComponent<Dunga>();
        }

        if (currentScene.name == "TutorialScene")
        {
            godCanvasUI.SetActive(false);
            foreach (GameObject enemy in currentStageEnemies)
            {
                dungaScript.GetComponent<Character>().health -= 25;
            }
             
        }

        if (currentScene.name == "Village")
        {
            godCanvasUI.SetActive(false);
        }

        if (currentScene.name == "VillageScene")
        {
            LoadGame();
            godCanvasUI.SetActive(false);
            Invoke("FadeOnOff", 2f);
        }

        if (currentScene.name == "OpeningScene")
        {
            LoadGame();
        }

        if (currentScene.name == "GameScene")
        {
            LoadGame();
        }

    }

    // Update is called once per frame
    void Update ()
    {
        //When Escape is pressed it starts the Pause method
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Paused");
            Pause();
        }

        //Debug.Log(counter); 
        foreach (GameObject enemy in currentStageEnemies)
        {
        ///Debug.Log(enemy.gameObject.name);
        }

        if (stageText != null)
        {
            CheckStageHealth();
            stageText.text = "Stage " + currentStage + " of 5";
            healthText.text = dungaScript.health + " / 100";
        }

        if (Input.GetKeyDown(KeyCode.F12))
            foreach (GameObject enemy in currentStageEnemies)
            {
                if (enemy.gameObject.name == "EnemyMarauder")
                {
                    enemy.GetComponent<EnemyMarauder>().health = 0;
                }

                if (enemy.gameObject.name == "EnemyArcher")
                {
                    enemy.GetComponent<EnemyArcher>().health = 0;
                }

                if (enemy.gameObject.name == "Chieftain")
                {
                    enemy.GetComponent<Boss_1>().health = 0;
                }


            }


        if (stageHealth <= 0 && combatActive == true)
        {
            Debug.Log("Stage Complete");
            StopCombat();
            dungaScript.ResetHealth();
            foreach (Transform child in dungaScript.buffBar.transform)
            {
                Destroy(child.gameObject);
            }

            if (currentStage == 1 && currentScene.name != "TutorialScene")
            {
                dialogueScript.Invoke("StartGameNarrative", 2.0f);
            }

            else if (currentStage == 2)
            {
                Invoke("NextStage", 2.0f);
                dungaScript.currentWaypoint = GameObject.Find("DungaWP3");
                dialogueScript.Invoke("StartGameNarrative", 8.0f);
                dialogueScript.Invoke("ToggleTimeline", 0f);
                dialogueScript.Invoke("ToggleTimeline", 7f);
            }

            else if (currentStage == 3)
            {
                Invoke("NextStage", 2.0f);
                dungaScript.currentWaypoint = GameObject.Find("DungaWP4");
                dialogueScript.Invoke("StartGameNarrative", 8.0f);
                dialogueScript.Invoke("ToggleTimeline", 0f);
                dialogueScript.Invoke("ToggleTimeline", 8f);
            }

            else if (currentStage == 4)
            {
                Invoke("NextStage", 2.0f);
                dungaScript.currentWaypoint = GameObject.Find("DungaWP5");
                dialogueScript.Invoke("StartGameNarrative", 8.0f);
                dialogueScript.Invoke("ToggleTimeline", 0f);
                dialogueScript.Invoke("ToggleTimeline", 6f);
            }

        }

        //if (Input.GetKeyUp(KeyCode.G))
        //{
        //    gameOver.SetActive(false);
        //    ani.SetBool("IsOpen", false);
        //}


        //if (GetComponent<Dunga>().health == 0)
        //{
        //}

        if(Input.GetKey(KeyCode.Space))
        {
            counter++;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            counter = 0;
        }

        if(counter >= 180)
        {
            if(currentScene.name == "OpeningScene")
            {
                EndOpeningScene();
            }

            if(currentScene.name == "TutorialScene")
            {
                EndTutorialScene();
            }

        }
    }

    public void ResetScene() {  
        
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Makes sure everything in pause menu is all in order
    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            if(pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                optionsMenu.gameObject.SetActive(false);
            }
            
            canvas.gameObject.SetActive(true);
            //Pauses the game when escape is pressed 
            Time.timeScale = 0;
            //Pauses the music when escape is pressed 
            AudioListener.volume = 0;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            //Continues the game when escape is pressed
            Time.timeScale = 1;
            //Continues the music when escape is pressed
            AudioListener.volume = 1;
        }

    }


    
    //Turns off the Canvas in pause menu
    public void StartButton()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    //Goes back to Main Menu
    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    //Opens Options menu/canvas
    public void Options(bool open)
    {
        Debug.Log(open);

        if (open)
        {
            optionsMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }

        if (!open)
        {
            optionsMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }

    public void ChangeDungaTarget()
    {
        dungaScript.GetTarget();
    }

    private void CheckStageHealth()
    {
        stageHealth = 0;

        SetCurrentStageEnemies();

        foreach (GameObject enemy in currentStageEnemies)
        {
            stageHealth += enemy.GetComponent<Character>().health;
        }
    }

    private void SetCurrentStageEnemies()
    {
        if (currentStage == 1)
        {
            currentStageEnemies = Stage1Enemies;
        }

        if (currentStage == 2)
        {
            currentStageEnemies = Stage2Enemies;
        }

        if (currentStage == 3)
        {
            currentStageEnemies = Stage3Enemies;
        }

        if (currentStage == 4)
        {
            currentStageEnemies = Stage4Enemies;
        }

        if (currentStage == 5)
        {
            currentStageEnemies = Stage5Enemies;
        }
    }

    public void NextStage()
    {
        currentStage++;
    }

    public void ChangeStageEnemySpeed(float speed)
    {
        foreach (GameObject enemy in currentStageEnemies)
        {
            enemy.GetComponent<Character>().moveSpeed = speed;
        }
    }

    public void HealthBarsOn()
    {
        dungaScript.healthbarCanvas.SetActive(true);
        dungaScript.GetTarget();

        foreach (GameObject enemy in currentStageEnemies)
        {
            enemy.GetComponent<Character>().healthbarCanvas.SetActive(true);
        }
    }

    public void ActivateCombat()
    {
        combatActive = true;
        dungaScript.healthbarCanvas.SetActive(true);
        dungaScript.combatActive = true;
        dungaScript.GetTarget();

        foreach (GameObject enemy in currentStageEnemies)
        {
            Debug.Log("Should be active");
            enemy.GetComponent<Character>().combatActive = true;
            enemy.GetComponent<Character>().healthbarCanvas.SetActive(true);
        }

    }

    public void StopCombat()
    {
        combatActive = false;
        dungaScript.healthbarCanvas.SetActive(false);
        dungaScript.combatActive = false;

        foreach (GameObject enemy in currentStageEnemies)
        {
            enemy.GetComponent<Character>().combatActive = false;
            enemy.GetComponent<Character>().healthbarCanvas.SetActive(false);
        }
}
		

    public void SetVolume(AudioSource source)
    {
        if (source == audioSrc)
            source.volume = slider.value;
        else if (source == audioSrc1)
            source.volume = slider1.value;
    }

    public void Mute()
    {
		audioSrc.volume = 0;
		audioSrc1.volume = 0;
    }

    public void EndTutorialScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndOpeningScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
        GodStats god = GameObject.Find("God").GetComponent<GodStats>();
        god.SavePlayer();
    }

    public void LoadGame()
    {
        GodStats god = GameObject.Find("God").GetComponent<GodStats>();
        god.LoadPlayer();

        GameData data = SaveSystem.LoadGame();

        slider.value = data.dialogueVolume;
        slider1.value = data.mainVolume;
        SetVolume(audioSrc);
        SetVolume(audioSrc1);
    }

    public void FreezeArrow()
    {
        Debug.Log("I froze arrow");
        frozenArrow = GameObject.Find("Arrow(Clone)");
        //arrowCollider = GameObject.Find("ArrowCircle");
        frozenArrow.GetComponent<Rigidbody>().velocity = Vector3.zero;
        frozenArrow.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(-transform.right, transform.up);
        frozenArrow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //focusCircle.SetActive(true);
    }

    public void UnFreezeArrow()
    {
        //frozenArrow = GameObject.Find("Arrow(Clone)");
        //arrowCollider = GameObject.Find("ArrowCircle");
        frozenArrow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GameObject.Find("DialogueManager").GetComponent<Dialogue>().Invoke("StartTutorialNarrative", 2.0f);
        //focusCircle.SetActive(true);
    }

    public void ToggleArrowCollider()
    {
        if (arrowCollider.activeInHierarchy)
        {
            arrowCollider.SetActive(false);
        }
        else
        { 
        arrowCollider.SetActive(true);
        }
    }

    public void FadeOnOff()
    {
        if (godCanvasFade.activeSelf == true)
        {
            godCanvasFade.SetActive(false);
        }

        else
        {
            godCanvasFade.SetActive(true);
        }

    }

}

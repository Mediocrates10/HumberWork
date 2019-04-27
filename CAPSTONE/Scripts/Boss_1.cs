using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;


public class Boss_1 : Character {

    public Dunga target;
    private bool DungaInRange = false;
    private float nextBarrier;
    private bool timerOn = false;
    public GameObject winCond;
    public Animator ani;
    private AudioSource audioSrc1;
    private PauseGame gm;

    [SerializeField]
   // private bool initialWalk;
    public Animator spriteAnim;
    //private float nextEnrage = 30.0f;

    public Rigidbody fireballPrefab;
    public Transform fireballSpawn;


    void Awake() {
        target = GameObject.FindObjectOfType<Dunga>();
        dialogueManager = GameObject.Find("DialogueManager");
        dialogueC = GameObject.Find("DialogueBox");
        anim = GetComponent<Animator>();
        crb = GetComponent<Rigidbody>();
        
    }


    // Use this for initialization
    void Start () {
        managerScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
        healthbarCanvas.SetActive(false);
        currentScene = SceneManager.GetActiveScene();
        health = startingHealth;
        nextBarrier = 4.0f;

        // set start position as current position, end position as Target
        startPos = transform.position;
        endPos = target.transform.position;

        InvokeRepeating("ShootFireball", 2f, 3f);
    }
	

	// Update is called once per frame
	void Update () {
        // fire the fireball
        if (combatActive == true)  {

            if (!timerOn)
            {
                StartCoroutine(BarrierBuffTimer());
            }


        }

        if (health <= 0)
        {
            //spriteAnimator.Play("Dying");
            Invoke("Kill",0.1f);
        }

    }

    IEnumerator BarrierBuffTimer()
    {
        timerOn = true;
        yield return new WaitForSeconds(nextBarrier);
        resetTimer();
    }

    private void resetTimer()
    {
        GameObject newEnrage = Instantiate(barrierPrefab, buffBar.transform.position, Quaternion.identity);
        newEnrage.transform.SetParent(buffBar.transform);
        nextBarrier = 15.0f;
        timerOn = false;
    }

    private void ShootFireball()
    {
            if (combatActive == true) {
                spriteAnimator.Play("ChieftainAttack");
                // instantiate arrow prefab at spawn position with proper rotation
                Rigidbody fireBallInstance;
                fireBallInstance = Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation) as Rigidbody;
                fireBallInstance.AddForce(-fireballSpawn.right * 500);
                //set rigid body to object's rigid body component
               
            }
    }

    protected override void Kill()
    {
        //GameObject.Find("Player").GetComponent<GodStats>().UpdatePower(500);

        gm = GameObject.Find("GameManager").GetComponent<PauseGame>();
        gm.SaveGame();
        GameObject.Find("DialogueManager").GetComponent<Dialogue>().FadeOnOff();
        GameObject.Find("BlackFade").GetComponent<Animator>().Play("FadeOut");
        Invoke("LoadTransition", 2f);
        Time.timeScale = 1;
    }

    private void LoadTransition()
    {
        SceneManager.LoadScene("Transition");
    }
}

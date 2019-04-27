using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyArcher : Character {

    //public GameObject enemy;

    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    private BoxCollider archerCollider;
    private bool shooting;
    private bool fireArrows = true;
    private Scene currenScene;

    // Use this for initialization
    void Start()
    {
        managerScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
        currenScene = SceneManager.GetActiveScene();
        gameManager = GameObject.Find("GameManager");
        archerCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        crb = GetComponent<Rigidbody>();
        startPos = transform.position;
        endPos = transform.position + Vector3.left * distance;
        health = startingHealth;
        healthbarCanvas.SetActive(false);

        // call LaunchArrow every 3 seconds after 2 seconds
    }

    // Update is called once per frame
    void Update()
    {
        if (combatActive == true)
        {
            if (shooting == false)
            {
                InvokeRepeating("LaunchArrow", 2f, 3f);
                shooting = true;
            }

            //Lerping into scene
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            //transform.position = Vector3.Lerp(startPos, endPos, perc);

            if (Input.GetButtonDown("Fire1"))
            {
                //TakeDamage(25);
            }
        }

        else
        {
            CancelInvoke("LaunchArrow");
            shooting = false;
        }

        if (isDead)
        {
            spriteAnimator.Play("ArcherDead");
        }

    }

    private void LaunchArrow()
    {
        if (fireArrows == true)
        {
        spriteAnimator.Play("ArcherAttack");
        // instantiate arrow prefab at spawn position with proper rotation
            GameObject go = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);

            // set rigid body to object's rigid body component
            Rigidbody rb = go.GetComponent<Rigidbody>();
        }

        if (currenScene.name == "TutorialScene")
        {
            fireArrows = false;
        }
    }

    protected override void Kill()
    {
        //GameObject.Find("Player").GetComponent<GodStats>().UpdatePower(100);

        gameManager.GetComponent<PauseGame>().ChangeDungaTarget();
        spriteAnchorAnimator.Play("DeathFall");
        combatActive = false;
        archerCollider.enabled = false;
        healthbarCanvas.SetActive(false);
        if (currenScene.name == "TutorialScene")
        {
            GameObject.Find("DialogueManager").GetComponent<Dialogue>().Invoke("StartTutorialNarrative", 1.0f);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyMarauder : Character {


    public Dunga target;

    public GameManager gm;
    private bool timerOn = false;
    public GameObject enragePrefab;
    public Transform initialWaypoint;
    private Vector3 walkDestination;
    private bool walkedTowardShip = false;
    private bool DungaInRange = false;
    [SerializeField]
    private bool initialWalk;
    private float nextEnrage = 30.0f;
    private BoxCollider marauderCollider;
    public Rigidbody fireballPrefab;
    public Transform fireballSpawn;


    private void Awake()
    {
        target = GameObject.FindObjectOfType<Dunga>();
        gameManager = GameObject.Find("GameManager");
        dialogueManager = GameObject.Find("DialogueManager");
        dialogueC = GameObject.Find("DialogueBox");
        anim = GetComponent<Animator>();
        crb = GetComponent<Rigidbody>();
        minDist = 2;
        moveSpeed = 5f;
    }

    // Use this for initialization
    void Start () {
        managerScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
        marauderCollider = GetComponent<BoxCollider>();
        initialWalk = false;
        walkDestination = initialWaypoint.transform.position;
        healthbarCanvas.SetActive(false);
        currentScene = SceneManager.GetActiveScene();
        health = startingHealth;
        nextEnrage = 3.0f;
        // set start position as current position, end position as Target
        startPos = transform.position;
        endPos = target.transform.position;

        if (currentScene.name == "GameScene")
        {
            Invoke("TurnAround", 3.25f);
        }

        InvokeRepeating("ShootFireball", 2f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (initialWalk == false && transform.position.x < walkDestination.x)
        {
            transform.position += transform.right * 1 * moveSpeed * Time.deltaTime;
            moveSpeed = 2.5f;
            spriteAnimator.Play("MarauderWalk");
        }

        else if (transform.position.x >= walkDestination.x)
        {
            initialWalk = true;
        }

        
        if (combatActive == true)
        {
            if (!timerOn)
            {
                StartCoroutine(EnrageBuffTimer());
            }

            if (Vector3.Distance(transform.position, target.transform.position) >= minDist)
            {
                DungaInRange = false;
                MoveTowards();
                // animate sprite
            }
            else
            {
                DungaInRange = true;
                // end animation
            }

            if (DungaInRange == true)
                Attack();
            
        }

        if (isDead)
        {
            spriteAnimator.Play("MarauderDead");
        }

        //if (Input.GetKeyDown(KeyCode.M))
        //{
          
        //    // instantiate arrow prefab at spawn position with proper rotation
        //    Rigidbody fireBallInstance;
        //    fireBallInstance = Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation) as Rigidbody;
        //    fireBallInstance.AddForce(fireballSpawn.forward * 25);
        //    // set rigid body to object's rigid body component
        //    //Rigidbody rb = go.GetComponent<Rigidbody>();
            
        //}
    }

    void MoveTowards()
    {
        transform.position += transform.right * -1 * moveSpeed * Time.deltaTime;
        spriteAnimator.Play("MarauderWalk");
    }

    void Attack()
    {
        if (Time.time > nextAttack)
        {
            spriteAnimator.Play("MarauderAttack");
            critCheck = Random.Range(1, 11);
            damage = Random.Range(15, 21);
            if (critCheck == 1)
            {
                damage *= 1.5f;
                damage = Mathf.Round(damage);
            }

            nextAttack = Time.time + attackRate;
            if(target.barrierActive == false)
                target.Damage(damage, critCheck);
            //Debug.Log("Damage passed to Dunga: " + damage);
        }
    }

    IEnumerator EnrageBuffTimer()
    {
        timerOn = true;
        yield return new WaitForSeconds(nextEnrage);
        resetTimer();
    }

    private void resetTimer()
    {
        GameObject newEnrage = Instantiate(enragePrefab, buffBar.transform.position, Quaternion.identity);
        newEnrage.transform.SetParent(buffBar.transform);
        nextEnrage = 20.0f;
    }

    private void TurnAround()
    {
        spriteAnchorAnimator.Play("TurnAround");
    }

    protected override void Kill()
    {    
        //GameObject.Find("Player").GetComponent<GodStats>().UpdatePower(100);
        gameManager.GetComponent<PauseGame>().ChangeDungaTarget();
        spriteAnchorAnimator.Play("DeathFall");
        combatActive = false;
        marauderCollider.enabled = false;
        //gm.eCount--;
        healthbarCanvas.SetActive(false);
    }

    private void ShootFireball()
    {
    //    if (combatActive == true)
    //    {
    //        // instantiate arrow prefab at spawn position with proper rotation
              Rigidbody fireBallInstance;
              //fireBallInstance = Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation) as Rigidbody;
              //fireBallInstance.AddForce(fireballSpawn.forward * 100);
              // set rigid body to object's rigid body component
    //        //Rigidbody rb = go.GetComponent<Rigidbody>();
    //    }
    }

    
}

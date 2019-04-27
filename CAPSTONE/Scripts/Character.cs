using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{

    #region Public Variables
    public bool combatActive = false;
    public float moveSpeed;
    public float health;
    public float startingHealth = 100;
    public Image healthFill;
    public Image healthBar;
    public Transform damageTextParent;
    public GameObject buffBar;
    public GameObject damageTextPrefab;
    public GameObject barrierPrefab;
    public GameObject dialogueManager;
    public GameObject healthbarCanvas;
    public TextMeshProUGUI uiHealthNumber;
    public Animator anim;
    public Animator spriteAnchorAnimator;
    public Animator spriteAnimator;
    //public Character character;
    //public GameManager gm;
    #endregion

    #region Protected Variables
    protected float damageTaken;
    protected float damageDealt;
    protected float damage;
    [SerializeField]
    protected float attackRate = 2f;
    protected float nextAttack = 0f;
    protected float distance;
    protected float lerpTime;
    protected float currentLerpTime;
    
    protected int critCheck;
    protected int minDist;

    protected bool targetInRange = false;
    protected bool shieldOn = false;
    protected bool isDead = false;

    protected PauseGame managerScript;
    protected GameObject gameManager;
    protected GameObject dialogueC;

    protected Vector3 startPos;
    protected Vector3 endPos;

    protected Rigidbody crb;
    protected Scene currentScene; 
    #endregion

    // Use this for initialization
    void Start () {
        health = startingHealth;
        //gameManager = GameObject.Find("GameController");
        
    }
	
	// Update is called once per frame
	void Update () {
		if (shieldOn == true)
        {
            //GameObject shield = Instantiate(Barr, arrowSpawn.position, Quaternion.identity);
        }
	}

    /*public virtual void Attack()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            character.Damage(damage, critCheck);
        }
    }*/

    public virtual void Damage(float damage, int critCheck)
    {
        health -= damage;
        damageTaken = damage;
        anim.Play("TakeDamage");
        DamageNumbers(damage, critCheck);
        if (health <= 0)
        {
            Kill();
            isDead = true;
        }
    }

    public void SetHealth(float num)
    {
        health = num;
    }

    public void DamageNumbers(float number, int critCheck)
    {
        float rnd = Random.Range(-0.5f, 0.5f);
        GameObject temp = Instantiate(damageTextPrefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        Animator tempAnim = temp.GetComponent<Animator>();
        TextMeshPro damageText = temp.GetComponent<TextMeshPro>();

        // Makes the popuptext gameobject a child of the appropriate parent
        temp.transform.SetParent(damageTextParent);

        tempRect.transform.SetSiblingIndex(0);

        tempRect.transform.localPosition = damageTextPrefab.transform.localPosition;
        tempRect.transform.localScale = damageTextPrefab.transform.localScale;

        //Sets the X Value to randomize slightly so damage numbers occur with some randomized horizontal distance
        float xPos = tempRect.transform.position.x + rnd;
        float yPos = tempRect.transform.position.y;
        tempRect.transform.position = new Vector2(xPos, yPos);

        damageText.text = "" + number;
        damageText.fontSize += number / 2;

        //Check for crit, if the attack was a crit it plays the crit animation instead
        if (critCheck == 1)
        {
            //damageText.color = Color.red;
            tempAnim.Play("CritAnimation");
        }

        Destroy(temp.gameObject, 1.0f);
    }

    private void TurnAround()
    {

    }

    public void ResetHealth()
    {
        health = startingHealth;
    }

    protected virtual void Kill()
    {
        Debug.Log("Kill called");
    }

    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }

    //Dialogue Activation 
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Dialogue")
        {
            dialogueC.SetActive(true);
            StartCoroutine(dialogueManager.GetComponent<DialogueTest>().Type());
            gameManager.GetComponent<PauseGame>().combatActive = false;
        }
    }

    public void ChangeAttackRate(float rate)
    {
        attackRate = rate;
    }

    private void OnMouseEnter()
    {
        if (managerScript.iconBeingDragged != null)
        {
            GetComponentInChildren<HealthBar>().barIndicator.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<HealthBar>().barIndicator.SetActive(false);
    }
}

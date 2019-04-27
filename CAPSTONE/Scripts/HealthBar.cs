using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Public Variables
    public Canvas objectCanvas;
    public GameObject barIndicator;
    public GameObject parentCharacter;
    public Transform parentPosition;
    public Collider ph_Col;
    public Sprite enemyBarSprite;
    public Sprite characterBarSprite;
    #endregion

    #region Private Variables
    private Scene currentScene;
    private GameObject myCharacter;
    private PauseGame managerScript;
    private RaycastHit hit;
    private Vector3 originalPosition;
    private Vector3 desiredPosition;
    private Vector3 direction;
    private Vector3 pos;
    private Vector3 rayStart;
    private Renderer Rend;
    private Image my_image;
    private Color startcolor;
    private Transform cam1;
    private LayerMask mask;
    private bool beingDragged = false;
    private float returnSpeed = 10;
    #endregion

    SkillCheck healthSkill;

    // Use this for initialization
    void Start () {
        managerScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
        //Call functions for initial values
        SetHealthBarPosition();
        currentScene = SceneManager.GetActiveScene();
        //Get Components attached to the Healthbar
        Rend = GetComponent<Renderer>();
        my_image = GetComponent<Image>();
        myCharacter = GameObject.Find("Character");
        cam1 = GameObject.Find("Main Camera").transform;    ///Finds the main camera - used to position UI correctly later on
        LayerMask lMask = LayerMask.GetMask("UI");          ///Sets the layermask variable to the UI layer - used to find collision with ui elements later on

        healthSkill = GameObject.Find("Health Skill").GetComponent<SkillCheck>();
    }
	
	// Update is called once per frame
	void Update () {
        SetDesiredPosition();
        if (transform.name == "PlayerHealthBar")
        {
            my_image.sprite = characterBarSprite;
        }
        else
        {
            my_image.sprite = enemyBarSprite;
        }

        if (beingDragged == false)
        {
            if (transform.position != desiredPosition)
            {
                direction = desiredPosition - transform.position;
                transform.Translate(direction * Time.deltaTime * returnSpeed);
            }
        }

    }

    void OnMouseEnter()
    {
        //startcolor = Rend.material.color;
        //Rend.material.color = Color.red;
    }
    void OnMouseExit()
    {
        //Rend.material.color = startcolor;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (currentScene.name != "TutorialScene" || currentScene.name == "TutorialScene" && !GameObject.Find("GameManager").GetComponent<PauseGame>().tutorialSwap)
        {
            Vector3 mousePos = eventData.position;
            mousePos.z -= (cam1.position.z - 0.1f);

            transform.position = Camera.main.ScreenToWorldPoint(mousePos);

            ph_Col.enabled = false;                 ///Turns off the collider
            beingDragged = true;
            managerScript.healthBarBeingDragged = gameObject;
            GetComponent<Image>().raycastTarget = false;
        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int layerMask = (1 << 5) | (1 << 9);

        rayStart = new Vector3(transform.position.x, transform.position.y, transform.position.z -5f);

        if (Physics.Raycast(rayStart, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            Character characterTemp = hit.collider.transform.gameObject.GetComponentInParent<Character>();
            if (hit.collider.transform.gameObject.GetComponentInParent<HealthBar>() != null)
            {
                hit.collider.transform.gameObject.GetComponentInParent<HealthBar>().barIndicator.SetActive(false);
            }

            if (healthSkill.skillActive)
            {
                Debug.Log("Skill Active");
                SwitchHealth(characterTemp);

                if (currentScene.name == "TutorialScene" && !GameObject.Find("GameManager").GetComponent<PauseGame>().tutorialSwap)
                {
                    GameObject.Find("GameManager").GetComponent<PauseGame>().tutorialSwap = true;
                    GameObject.Find("DialogueManager").GetComponent<Dialogue>().Invoke("StartTutorialNarrative", 1f);
                }
            }

           
        }
        ph_Col.enabled = true;                  ///Turns on the collider
        beingDragged = false;
        managerScript.healthBarBeingDragged = null;
        GetComponent<Image>().raycastTarget = true;
        //transform.position = originalPosition;  ///Resets position to above the character
    }

    private void SwitchHealth(Character characterTemp)
    {

        float tempHealth = parentCharacter.GetComponent<Character>().health;

        parentCharacter.GetComponent<Character>().health = characterTemp.health;

        characterTemp.health = tempHealth;
    }

    public void SetHealthBarPosition()
    {
        originalPosition = parentPosition.position;
        originalPosition.y += 2.5f;
        transform.position = originalPosition;
        desiredPosition = originalPosition;
    }

    public void SetDesiredPosition()
    {
        desiredPosition = parentPosition.position;
        desiredPosition.y += 2.5f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (managerScript.healthBarBeingDragged != null && managerScript.healthBarBeingDragged != gameObject)
        {
            barIndicator.SetActive(true);
        }

        if (managerScript.iconBeingDragged != null)
        {
            barIndicator.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        barIndicator.SetActive(false);
    }
}

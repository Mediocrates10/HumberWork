using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class TutorialBuffIcon : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private PauseGame managerScript;
    private Transform camLocation;
    private Vector3 originalPosition;
    private Vector3 direction;
    private Vector3 rayStart;
    private Vector3 desiredPosition;
    private RaycastHit hit;
    private TextMeshPro countdownText;
    private bool beingDragged = false;
    public bool active;
    private float returnSpeed = 10.0f;
    private float destroyTimer;
    public Collider iColl;

    SkillCheck buffSkill;

    // Use this for initialization
    void Start()
    {
        managerScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
        active = false;
        Time.timeScale = 1.0f;
        camLocation = GameObject.Find("Main Camera").transform;    ///Finds the main camera
        countdownText = GetComponentInChildren<TextMeshPro>();
        SetPosition();
        iColl = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDesiredPosition();
        if (beingDragged == false)
        {
            if (transform.position != desiredPosition)
            {
                direction = desiredPosition - transform.position;
                transform.Translate(direction * Time.deltaTime * returnSpeed);
            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (active)
        {
            Vector3 mousePos = eventData.position;
            mousePos.z -= (camLocation.position.z - 0.1f);
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            iColl.enabled = false;
            beingDragged = true;
            managerScript.iconBeingDragged = gameObject;
            GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var layerMask = ~(1 << 13);
        rayStart = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5f);

        if (Physics.Raycast(rayStart, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject.tag == "Player" && active)
            {
                Destroy(GameObject.Find("Canvas/CharacterBar/BuffBar/iconPrefab(Clone)"));
                transform.SetParent(hit.transform.Find("Canvas/CharacterBar/BuffBar"));
                GameObject.Find("DialogueManager").GetComponent<Dialogue>().Invoke("StartTutorialNarrative", 1.0f);
                active = false;                
                //This statement is used to destroy the shield on target, but target should never have one since it is destroyed when it moves to another character
                //Only reason this is kept is for the possibility of 2 shields being active - something not implemented currently
                /*if (hit.transform.Find("Canvas/CharacterBar/BuffBar/BarrierPrefab(Clone)"))
                {
                    //GameObject temp = hit.transform.Find("Canvas/CharacterBar/BuffBar/BarrierPrefab(Clone)").gameObject;
                    //Destroy(temp);
                }*/
            }

            else if (hit.transform.GetComponentInParent<Dunga>() && active)
            {
                Destroy(GameObject.Find("Canvas/CharacterBar/BuffBar/iconPrefab(Clone)"));
                transform.SetParent(hit.transform.parent.Find("BuffBar"));
                GameObject.Find("DialogueManager").GetComponent<Dialogue>().Invoke("StartTutorialNarrative", 1.0f);
                active = false;
            }

        }

        iColl.enabled = true;                  ///Turns on the collider

        beingDragged = false;

        managerScript.iconBeingDragged = null;

        GetComponent<Image>().raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    private void SetPosition()
    {
        originalPosition = transform.parent.position;
        transform.position = originalPosition;
    }

    private void SetDesiredPosition()
    {
        desiredPosition = transform.parent.position;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

}

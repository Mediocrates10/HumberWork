using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class EnrageIcon : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
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
    private float returnSpeed = 10.0f;
    private float destroyTimer;

    public GameObject enragePrefab;
    public Collider iColl;

    SkillCheck buffSkill;

    // Use this for initialization
    void Start () {
        managerScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
        GameObject newEnrage = Instantiate(enragePrefab, transform.position, Quaternion.identity);
        newEnrage.transform.SetParent(transform.parent);
        newEnrage.GetComponent<Enrage_Buff>().SetPosition();
        Time.timeScale = 1.0f;
        camLocation = GameObject.Find("Main Camera").transform;    ///Finds the main camera
        countdownText = GetComponentInChildren<TextMeshPro>();
        SetPosition();
        iColl = GetComponent<Collider>();
        destroyTimer = 10.0f;
        Invoke("Destroy", destroyTimer);
        InvokeRepeating("SubtractTime", 0, 0.1f);

        buffSkill = GameObject.Find("Buff Skill").GetComponent<SkillCheck>();
    }

    // Update is called once per frame
    void Update () {
        SetDesiredPosition();
        countdownText.text = destroyTimer.ToString("F0");
        if (beingDragged == false)
        {
            if (transform.position != desiredPosition)
            {
                    direction = desiredPosition - transform.position;
                    transform.Translate(direction * Time.deltaTime * returnSpeed);
            }
        }

        if (GetComponentInParent<Dunga>() != null)
        {
            GetComponentInParent<Dunga>().ChangeAttackRate(1.0f);
        }
        else if (GetComponentInParent<EnemyMarauder>() != null)
        {
            GetComponentInParent<EnemyMarauder>().ChangeAttackRate(1.0f);
        }
        else if (GetComponentInParent<EnemyArcher>() != null)
        {
            GetComponentInParent<EnemyArcher>().ChangeAttackRate(1.0f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (buffSkill.skillActive == true)
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
            Debug.Log(hit.collider.gameObject);
            if (hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "Enemy")
            {
                GetComponentInParent<Character>().ChangeAttackRate(2.0f);
                Destroy(GameObject.Find("Canvas/CharacterBar/BuffBar/EnragePrefab(Clone)"));
                transform.SetParent(hit.transform.Find("Canvas/CharacterBar/BuffBar"));
                GameObject newEnrage = Instantiate(enragePrefab, transform.position, Quaternion.identity);
                newEnrage.transform.SetParent(transform.parent);
                //This statement is used to destroy the shield on target, but target should never have one since it is destroyed when it moves to another character
                //Only reason this is kept is for the possibility of 2 shields being active - something not implemented currently
                /*if (hit.transform.Find("Canvas/CharacterBar/BuffBar/BarrierPrefab(Clone)"))
                {
                    //GameObject temp = hit.transform.Find("Canvas/CharacterBar/BuffBar/BarrierPrefab(Clone)").gameObject;
                    //Destroy(temp);
                }*/
            }

            else if (hit.transform.gameObject.name == "HealthBarBG")
            {
                Destroy(GameObject.Find("Canvas/CharacterBar/BuffBar/EnragePrefab(Clone)"));
                transform.SetParent(hit.transform.parent.Find("BuffBar"));
                GameObject newEnrage = Instantiate(enragePrefab, transform.position, Quaternion.identity);
                newEnrage.transform.SetParent(transform.parent);
            }

        }

        iColl.enabled = true;                  ///Turns on the collider

        beingDragged = false;

        GetComponent<Image>().raycastTarget = true;
        managerScript.iconBeingDragged = null;
    }

    private void SubtractTime()
    {
        if (destroyTimer > 0.1f)
        {
            destroyTimer -= 0.1f;
            destroyTimer = Mathf.Round(destroyTimer * 100f) / 100f;
        }
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
        GetComponentInParent<Character>().ChangeAttackRate(2.0f);
        Destroy(gameObject);
        Destroy(GameObject.Find("Canvas/CharacterBar/BuffBar/EnragePrefab(Clone)"));
    }

}

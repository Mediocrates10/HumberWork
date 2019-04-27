using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ArrowCircle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private Animator anim;
    public Projectile arrowScript;
    private bool clickable;
    private bool stopped;
    private bool shootable;
    private bool shot;
    private Vector3 mousePos;
    private Scene currentScene;
    private PauseGame managerScript;
    GodStats god;
    SkillCheck arrowSkill;

	// Use this for initialization
	void Start () {
        managerScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
        currentScene = SceneManager.GetActiveScene();
        clickable = false;
        stopped = false;
        shootable = false;
        shot = false;
        anim = gameObject.GetComponent<Animator>();
        arrowSkill = GameObject.Find("Arrow Skill").GetComponent<SkillCheck>();
        if (currentScene.name == "TutorialScene")
        {
            managerScript.arrowCollider = gameObject;
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (arrowScript.hitCheck == false)
            //arrowScript.arrowBody.transform.rotation = Quaternion.LookRotation(arrowScript.arrowBody.velocity);

        if (Input.GetMouseButtonDown(0) && clickable && !stopped)
        {
            if (currentScene.name == "TutorialScene")
            {
                    managerScript.UnFreezeArrow();

            }

            arrowScript.arrowBody.velocity = new Vector3(-.1f, -.1f, 0);
            anim.Play("HideCircle");
            stopped = true;
        }

        //else if (Input.GetMouseButtonDown(0) && stopped && clickable)
        //{
        //    arrowScript.hitCheck = true;
        //    arrowScript.arrowBody.isKinematic = true;
        //    shootable = true;
        //}

        //if (shootable == true)
        //{
        //    mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 9.7f));
        //    arrowScript.transform.LookAt(mousePos);

        //    if (shot == true && Input.GetMouseButtonDown(0))
        //    {
        //        Debug.Log("Clicked");
        //        arrowScript.arrowBody.isKinematic = false;
        //        Vector3 direction = mousePos - transform.position;
        //        direction.Normalize();
        //        arrowScript.arrowBody.velocity = new Vector3(20, direction.y);
        //        shootable = false;
        //    }

        //    shot = true;
        //}
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(arrowSkill.skillActive);
        if (arrowSkill.skillActive == true && !stopped)
        {
            anim.Play("ShowCircle");
            clickable = true;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        clickable = false;
        anim.Play("HideCircle");
    }
}

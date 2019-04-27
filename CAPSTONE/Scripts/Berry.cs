using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Berry : MonoBehaviour
{
    public Animator anim;
    Rigidbody rb;

    float drop;

    private Vector3 mousePos;
    private bool offBranch = false;
    private bool beingDragged = false;
    private Transform cam1;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        cam1 = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        RaycastHit hitI = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitI))
        {
            if (hitI.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Add Health");
                Destroy(gameObject);
            }
        }

        RaycastHit hitE = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitE))
        {
            if (hitE.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Remove Health");
                Destroy(gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag("Branch"))
                {
                    Debug.Log("Dog hit");
                    anim.SetTrigger("Click");
                    drop++;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetTrigger("UpClick");
        }

        if (drop == 3)
        {
            rb.useGravity = true;
            gameObject.GetComponent<Animator>().enabled = false;
            offBranch = true;
        }

    }

    public void OnMouseDrag()
    {
        if (offBranch == true)
        {
            mousePos = Input.mousePosition;
            mousePos.z -= (cam1.position.z + 0.1f);
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log("Dragged");
        }
    }

}

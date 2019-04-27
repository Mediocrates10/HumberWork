using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BranchShake : MonoBehaviour
{

    Rigidbody rb;
    Rigidbody rb1;

    float drop;

    private Vector3 rayStart;
    private RaycastHit hit;

    public Animator anim;
    public Collider branch;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb1 = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetTrigger("UpClick");
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag("Branch"))
                {
                    Debug.Log("Branch hit");
                    anim.SetTrigger("Click");
                }
            }
        }
    }

}

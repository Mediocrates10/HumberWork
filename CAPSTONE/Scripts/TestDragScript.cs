using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestDragScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Transform cam1;

    // Use this for initialization
    void Start()
    {
    cam1 = GameObject.Find("Main Camera").transform;    ///Finds the main camera - used to position UI correctly later on
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDrag()
    {
        mousePos = Input.mousePosition;
        mousePos.z -= (cam1.position.z + 0.1f);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log("Dragged");
    }

}

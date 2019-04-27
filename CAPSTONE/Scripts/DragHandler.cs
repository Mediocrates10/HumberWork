using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler {


    Vector3 originalPosition;
    public Transform parentPosition;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = eventData.position;
        mousePos.z = 10;
        //Debug.Log("Mouse Position = " + mousePos + "\n" + "World Position = " + Camera.main.ScreenToWorldPoint(mousePos));
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originalPosition;
    }

    public void SetHealthBarPosition()
    {
        originalPosition = parentPosition.position;
        originalPosition.y += 3;
        transform.position = originalPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    public GameObject canvas;
    public GameObject shopMenu;
    public PauseGame gm;

    // Use this for initialization
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<PauseGame>();
        gm.menuActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnMouseEnter()
    {
        canvas.SetActive(true);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !gm.menuActive)
        {
            shopMenu.SetActive(true);
            gm.menuActive = true;
        }
    }

    private void OnMouseExit()
    {
        canvas.SetActive(false);
    }

    public void ExitShop()
    {
        shopMenu.SetActive(false);
        gm.menuActive = false;
    }
}

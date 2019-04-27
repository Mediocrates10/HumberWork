using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarFill : MonoBehaviour {

    private Character mycharacter;
    public Image healthFill;
	// Use this for initialization
	void Start () {
        mycharacter = transform.parent.parent.parent.parent.gameObject.GetComponent<Character>();
    }
	
	// Update is called once per frame
	void Update () {
        healthFill.fillAmount = mycharacter.health / 100;
	}
}

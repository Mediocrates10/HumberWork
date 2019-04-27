using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass_broken : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {

        Time.timeScale = 0.5f;
        // After 2 sec reset time scale and destory gameobject
        Invoke("Despwan", 3.0f);

    }

    void Despwan()
    {
        Time.timeScale = 1;
        Destroy(gameObject);

    }
}

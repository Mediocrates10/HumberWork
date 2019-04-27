using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Camera_con : MonoBehaviour {

   // public List<Transform> cameraLocs;

    bool changeLevel = false;

	// Use this for initialization
	void Start () {
        Debug.Log("Opening animation plays here");
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("CameraPan", 3);
        Invoke("Stop", 13);
    }


    void CameraPan() {
        transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y);

    }

    void Stop() {

        //Destroy(gameObject);
        SceneManager.LoadScene(2);
    }
}

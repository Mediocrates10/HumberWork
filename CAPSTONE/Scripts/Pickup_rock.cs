using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_rock : MonoBehaviour {

    public GameObject rockPrefab;
    public Transform spawnLoc;
    private Transform cameraLocation;
    private GameObject gameManager;

	// Use this for initialization
	void Start () {
        cameraLocation = GameObject.Find("Main Camera").transform;
        gameManager = GameObject.Find("GameManager");
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && gameManager.GetComponent<PauseGame>().combatActive)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Spawning_Stone")
                {
                    GameObject newRock = Instantiate(rockPrefab, spawnLoc.position, Quaternion.identity);
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane - cameraLocation.position.z));
                    newRock.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
                    newRock.transform.Rotate(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
                }
            }
        }
    }
}

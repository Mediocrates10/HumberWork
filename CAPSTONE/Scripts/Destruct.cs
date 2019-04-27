using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruct : MonoBehaviour {

    public GameObject broken;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(broken, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

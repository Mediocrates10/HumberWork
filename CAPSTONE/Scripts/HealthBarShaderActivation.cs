using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarShaderActivation : MonoBehaviour {
    //public GameObject iHealth;
    public float iSpeed;

    public Material mMaterial;
    private float mTime;

    // Use this for initialization
    void Start()
    {
        //mMaterial = iHealth.GetComponent<Renderer>().material;

        mTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mTime -= Time.deltaTime * iSpeed;

        mMaterial.SetFloat("_Offset", Mathf.Repeat(mTime, 1.0f));
    }
}

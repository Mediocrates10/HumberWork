using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_Buff : MonoBehaviour {

    private float iSpeed = 0.5f;
    private float mTime;
    private Material mMaterial;
    private Vector3 Position;

    // Use this for initialization
    void Start()
    {
        mMaterial = GetComponent<MeshRenderer>().material;
        mTime = 0.0f;
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        mTime += Time.deltaTime * iSpeed;
        mMaterial.SetFloat("_Offset", Mathf.Repeat(mTime, 1.0f));
    }

    public void SetPosition()
    {
        Position = (transform.parent.parent.parent.parent.position);
        Position.y -= 0.1f;
        transform.position = Position;
    }
}
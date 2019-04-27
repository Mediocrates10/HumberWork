using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enrage_Buff : MonoBehaviour
{

    private float iSpeed = 0.5f;
    private float mTime;
    private Material mMaterial;
    private Vector3 Position;

    // Use this for initialization
    void Start()
    {
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPosition()
    {
        Position = (transform.parent.parent.parent.parent.position);
        Position.y -= 0.1f;
        transform.position = Position;
    }
}
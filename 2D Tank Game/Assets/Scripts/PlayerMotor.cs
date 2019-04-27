using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMotor : MonoBehaviour {

    private Vector2 velocity = Vector2.zero;
    private Vector3 rotation = Vector3.zero;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Gets movement vector
    public void Move (Vector2 velocity_)
    {
        velocity = velocity_;
    }
    // Gets rotation vector
    public void Rotate(Vector3 rotation_)
    {
        rotation = rotation_;
    }

    // Run all physics
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    // Move based on velocity
    void PerformMovement()
    {
        if (velocity != Vector2.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    // Rotate 
    void PerformRotation()
    {
        rb.transform.Rotate(rotation);
    }
}

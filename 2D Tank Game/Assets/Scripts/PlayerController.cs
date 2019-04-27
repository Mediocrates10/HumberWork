using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float turnSensitivity = 3f;
    private PlayerMotor motor;
    Vector3 rotation;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rotation = Vector3.zero;
    }
    void Update()
    {
        // Calculate velocity as vector
        float yMov = Input.GetAxisRaw("Vertical");

        Vector2 verticalMov = transform.up * yMov;

        // Final velocity vector
        Vector2 velocity = verticalMov.normalized * speed;

        // Apply movement 
        motor.Move(velocity);

        // Calculate rotation as vector
        float zRot = Input.GetAxisRaw("Rotate");

        rotation = new Vector3(0f, 0f, zRot) * turnSensitivity;

        // Apply rotation
        motor.Rotate(rotation);
    }
}

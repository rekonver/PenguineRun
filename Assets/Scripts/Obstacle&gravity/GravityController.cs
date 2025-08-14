using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float gravityStrength = 9.81f; // Strength of gravity

    void Start()
    {
        // Set initial gravity
        UpdateGravity();
    }

    void FixedUpdate()
    {
        // Update the gravity to always point downwards relative to the character's orientation
        UpdateGravity();

        // Visualize the gravity direction in the scene
        Debug.DrawRay(transform.position, Physics.gravity.normalized * 2, Color.red);
        Debug.Log("Current gravity: " + Physics.gravity);
    }

    // Method to update gravity direction based on the character's local down direction
    public void UpdateGravity()
    {
        Vector3 localDown = transform.TransformDirection(Vector3.down); // Get the local down direction
        Physics.gravity = localDown.normalized * gravityStrength; // Set global gravity
    }

    // Method to change gravity strength
    public void SetGravityStrength(float newStrength)
    {
        gravityStrength = newStrength;
        UpdateGravity();
    }
}

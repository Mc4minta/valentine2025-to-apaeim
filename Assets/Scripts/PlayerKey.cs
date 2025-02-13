using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    public float playerSpeed = 5f; // Movement speed
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement; // Store movement input

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get WASD/Arrow key input
        movement.x = Input.GetAxisRaw("Horizontal"); // A (-1) / D (+1)
        movement.y = Input.GetAxisRaw("Vertical");   // W (+1) / S (-1)

        // Normalize diagonal movement
        movement = movement.normalized;

        // Handle animations
        if (movement != Vector2.zero)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("Direction", movement.x >= 0 ? 1f : -1f); // Flip based on direction
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = movement * playerSpeed;
    }
}

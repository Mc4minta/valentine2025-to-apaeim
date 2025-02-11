using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 movement = movementJoystick.joystickVec;
            
        if(movementJoystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed,
                movementJoystick.joystickVec.y * playerSpeed);

            animator.SetBool("IsWalking", true);

            float direction = movement.x > 0 ? 1f : -1f;

            animator.SetFloat("Direction", direction);

        }

        else
        {
            rb.velocity = Vector2.zero;

            animator.SetBool("IsWalking", false);
        }
    }

}

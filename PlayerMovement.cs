using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    // reference to the Rigidbody2D component attached to the player object. Rigidbody2D allows us to apply physics-based movement.
    public Rigidbody2D rb;
    [Header("Movement")]
    // determines the player’s movement speed
    public float moveSpeed = 5f;
    // local variable stores the horizontal input value (left or right movement)
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("TouchingGround")]
    public Transform touchingGroundPos;
    public Vector2 touchingGroundSize = new Vector2(0.5f, 0.05f);
    public LayerMask GroundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    void Update() // Update is called once per frame
    // sets the player’s velocity based on the horizontal input (horizontalMovement) and the defined moveSpeed
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        Grounded();
        Gravity();
    }

    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            // fall speed increases as yu fall
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;
            }
            else if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpsRemaining--;
            }
        }
    }

    private void Grounded()
    {
        if (Physics2D.OverlapBox(touchingGroundPos.position, touchingGroundSize, 0, GroundLayer))
        {
            jumpsRemaining = maxJumps;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(touchingGroundPos.position, touchingGroundSize);
    }
}

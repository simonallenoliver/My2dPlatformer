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

    [Header("TouchingGround")]
    public Transform touchingGroundPos;
    public Vector2 touchingGroundSize = new Vector2(0.5f, 0.05f);
    public LayerMask GroundLayer;

    void Update() // Update is called once per frame
    // sets the player’s velocity based on the horizontal input (horizontalMovement) and the defined moveSpeed
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    // holding down the jump button will be a full jump
    // quick tap will be half jump
    {

        if (isGrounded())
        {
            if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            else if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
        }
    }

    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(touchingGroundPos.position, touchingGroundSize, 0, GroundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(touchingGroundPos.position, touchingGroundSize);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // reference to the Rigidbody2D component attached to the player object. Rigidbody2D allows us to apply physics-based movement.
    public Rigidbody2D rb;

    // determines the player’s movement speed
    public float moveSpeed = 5f;

    // local variable stores the horizontal input value (left or right movement)
    float horizontalMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    // sets the player’s velocity based on the horizontal input (horizontalMovement) and the defined moveSpeed
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
}

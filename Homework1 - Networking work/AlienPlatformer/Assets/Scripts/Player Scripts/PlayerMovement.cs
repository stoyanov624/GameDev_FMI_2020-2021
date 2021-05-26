using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkBehaviour {
    
    [Header("Running")]
    [SerializeField] private float movementSpeed = 16f;
    [SerializeField] private float linearDrag = 10f;
    [SerializeField] private float maxSpeed = 8f; // limiting the addForce.
    private float moveDirection = 1;
    private Vector2 inputVector;
    private int xInput;
    private bool shouldFlip => (xInput != 0.0f && xInput != moveDirection);
    private bool isGrounded;

    [Header("Components")]
    Rigidbody2D playerRB;

    private void Start() {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(IsOwner) {
            GetInput();
            xInput = (int)(inputVector * Vector2.right).normalized.x;
        }
    }

    private void FixedUpdate() {
        if(IsOwner) {
            isGrounded = PlayerCollision.isGroundedCheckDelegate;
            
            MoveCharacter();
            if(isGrounded) {
                ApplyGroundLinearDrag();
            }
        }
    }

    private void MoveCharacter() {
        playerRB.AddForce(new Vector2(xInput, 0.0f) * movementSpeed);
        
        if(Mathf.Abs(playerRB.velocity.x) > maxSpeed) {
            playerRB.velocity = new Vector2(Mathf.Sign(playerRB.velocity.x) * maxSpeed,playerRB.velocity.y);
        }
        
        if(shouldFlip) {
            Flip();
        }
    }

    private void ApplyGroundLinearDrag() {
        if ((Mathf.Abs(xInput) < 0.4f && playerRB.velocity.y < 1) || shouldFlip) {
            playerRB.drag = linearDrag;
        }else {
            playerRB.drag = 0.0f;
        }
    }

    private void Flip() { 
        moveDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void GetInput() {
        inputVector =  new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
}

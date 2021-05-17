using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 12f; 
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMiltiplier = 5f;
    [SerializeField] private float lowJumpMultiplier = 3f;
    private float jumpDelay = 0.25f;
    private float jumpTimer; 
    private bool canJumpTwice = true;
    private bool isGrounded = false;
    private bool jumpKeyPressed = false;

    [Header("Components")]
    private Rigidbody2D playerRB;

    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
        if(Input.GetButtonDown("Jump")) {
            jumpKeyPressed = true;
            jumpTimer = Time.time + jumpDelay;
        }
    }

    private void FixedUpdate() {
        isGrounded = (bool)PlayerCollision.isGroundedCheckDelegate?.Invoke();

        if(!isGrounded) {
            ApplyAirLinearDrag();
        }
        HandleJump();
        modifyJump();
    }

     private void ApplyAirLinearDrag() {
        playerRB.drag = airLinearDrag;
    }

    private void HandleJump() {
        if(jumpKeyPressed && jumpTimer > Time.time) {
            if(isGrounded) {
                Jump();
                canJumpTwice = true;
            }else if(!isGrounded && canJumpTwice) {
                Jump();
                canJumpTwice = false;
            }
        }
    }

    private void Jump() {
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpKeyPressed = false;
        jumpTimer = 0;
    }


    private void modifyJump() {
        if(playerRB.velocity.y < 0) {
            playerRB.gravityScale = fallMiltiplier;
        }
        else if(playerRB.velocity.y > 0 && !jumpKeyPressed) {
            playerRB.gravityScale = lowJumpMultiplier;
        } 
        else {
            playerRB.gravityScale = 1f;
        }
    }
}

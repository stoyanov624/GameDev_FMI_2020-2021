using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {
    
    [SerializeField] internal PlayerMovement playerScript;
    [SerializeField] internal PlayerInput playerInputScript;
    [SerializeField] internal PlayerCollision collisionScript;

    [Header("Jumping")]
    [SerializeField] internal float jumpForce = 12f; 
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMiltiplier = 5f;
    [SerializeField] private float lowJumpMultiplier = 3f;
    private float jumpDelay = 0.25f;
    private float jumpTimer; 
    private bool canJumpTwice = true;

    void Start() {

    }

    void Update() {
    }

    void FixedUpdate() {
        if(playerInputScript.isJumping) {
            jumpTimer = Time.time + jumpDelay;
           if(collisionScript.isGrounded()) {
                canJumpTwice = true;
                Jump();
            }else if (!collisionScript.isOnGround && canJumpTwice) {
                canJumpTwice = false;
                Jump();
            }
        }
        
        modifyJump();
    }
    
    private void Jump() {
        playerScript.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    private void modifyJump() {
        if(collisionScript.isOnFalling) {
            playerScript.rb.gravityScale = fallMiltiplier + 2f;
        } else {
            if(playerScript.rb.velocity.y < 0) {
                playerScript.rb.gravityScale = fallMiltiplier;
            }
            else if(playerScript.rb.velocity.y > 0 && !playerInputScript.isJumping) {
                playerScript.rb.gravityScale = lowJumpMultiplier;
            } 
            else {
            playerScript.rb.gravityScale = 1.25f;
            }
        }
    }

    internal void ApplyAirLinearDrag() {
        playerScript.rb.drag = airLinearDrag;
    }
}

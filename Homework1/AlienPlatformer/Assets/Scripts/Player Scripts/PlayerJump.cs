using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {
    
    [SerializeField] internal PlayerScript playerScript;
    [SerializeField] internal PlayerCollision collisionScript;

    [Header("Jumping")]
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMiltiplier = 5f;
    [SerializeField] private float lowJumpMultiplier = 3f;
    [SerializeField] internal float jumpForce = 12f; 
    private float jumpDelay = 0.25f;
    private bool isJumping = false;
    private float jumpTimer; 
    private bool canJumpTwice = true;


    void Update () {
        if(Input.GetKeyDown("w")) {
            isJumping = true;
            jumpTimer = Time.time + jumpDelay;
        }
    }
    void FixedUpdate() {
        if(isJumping) {
           if(jumpTimer > Time.time && collisionScript.isOnGround) {
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
        playerScript.playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = false;
        jumpTimer = 0;
    }

    private void modifyJump() {
        if(collisionScript.isOnFalling) {
            playerScript.playerRb.gravityScale = fallMiltiplier + 2f;
        } else {
            if(playerScript.playerRb.velocity.y < 0) {
                playerScript.playerRb.gravityScale = fallMiltiplier;
            }
            else if(playerScript.playerRb.velocity.y > 0 && !isJumping) {
                playerScript.playerRb.gravityScale = lowJumpMultiplier;
            } 
            else {
            playerScript.playerRb.gravityScale = 1.25f;
            }
        }
    }

    internal void ApplyAirLinearDrag() {
        playerScript.playerRb.drag = airLinearDrag;
    }
}

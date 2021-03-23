using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    [SerializeField] internal PlayerRunning runningScript;
    [SerializeField] internal PlayerJump jumpingScript;
    [SerializeField] internal PlayerCollision collisionScript;


    [Header("Components")]
    [SerializeField] private Animator animator;
    internal Rigidbody2D playerRb;

    void Awake(){
        playerRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if(collisionScript.isOnGround) {
            runningScript.ApplyGroundLinearDrag();
        }else {
            jumpingScript.ApplyAirLinearDrag();
        }
    }

    private void Update() {
        AnimateMovement();
    }

    private void AnimateMovement() {
        animator.SetFloat("vertical",Mathf.Abs(playerRb.velocity.y));

        if(collisionScript.isOnGround && runningScript.isRunning) {
            animator.Play("running");
        }else if(collisionScript.isOnGround && !runningScript.isRunning) {
            animator.Play("idle" );
        }
    }
}

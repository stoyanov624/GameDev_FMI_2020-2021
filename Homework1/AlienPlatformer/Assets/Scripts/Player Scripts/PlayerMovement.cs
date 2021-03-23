using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] internal PlayerRunning runningScript;
    [SerializeField] internal PlayerInput inputScript;
    [SerializeField] internal PlayerJump jumpingScript;
    [SerializeField] internal PlayerCollision collisionScript;


    [Header("Components")]
    [SerializeField] private Animator animator;
    internal Rigidbody2D rb;
    internal BoxCollider2D boxCollider2D;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
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
        animator.SetFloat("vertical",Mathf.Abs(rb.velocity.y));

        if(collisionScript.isOnGround && inputScript.isRunning) {
            animator.Play("running");
        }else if(collisionScript.isOnGround && !inputScript.isRunning) {
            animator.Play("idle" );
        }
    }
}

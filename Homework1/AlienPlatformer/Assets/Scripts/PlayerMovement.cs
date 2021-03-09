using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpDelay = 0.25f;
    private float jumpTimer; 
    private bool facingRight = true;
    
    [Header("Physics")]
    [SerializeField]
    private float maxSpeed; // limiting the addForce.
    [SerializeField]
    private float linearDrag;
    private float gravity = 1.0f; // we will apply gravity the moment our character jumps
    [SerializeField]
    private float fallMiltiplier = 6f; // we will multiply the gravity pull so our character comes down.

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private LayerMask groundLayerMask;
    private Vector2 horizontalDirection;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update() {
        horizontalDirection = new Vector2(Input.GetAxis("Horizontal"),0);
        if(Input.GetKeyDown("w")) {
            jumpTimer = Time.time + jumpDelay;
        }
    }

    void FixedUpdate() {
        if(jumpTimer > Time.time && isGrounded()) {
            Jump();
        }

        MoveCharacter(horizontalDirection.x);
        modifyPhysics();
    }
    
    void MoveCharacter(float horizontalDirection) {
        rb.AddForce(Vector2.right * horizontalDirection * movementSpeed);
        AnimateMovement();
        
        if(Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed,rb.velocity.y);
        }
        
        if((horizontalDirection > 0 && !facingRight) || (horizontalDirection < 0 && facingRight)) {
            Flip();
        }
    }

    private void Jump() {
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    private void modifyPhysics() {
        bool changingDirection = (horizontalDirection.x > 0 && rb.velocity.x < 0) || (horizontalDirection.x < 0 && rb.velocity.x > 0);

        if(isGrounded()) {
            if(Mathf.Abs(horizontalDirection.x) < (linearDrag / 11f) || changingDirection) {
                rb.drag = linearDrag;
            } else {
                rb.drag = 0;
            }
            rb.gravityScale = 0;
        } else {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.2f;
            if(rb.velocity.y < 0) {
                rb.gravityScale = gravity * fallMiltiplier;
            }
            else if (rb.velocity.y >= 0 && !Input.GetKeyDown("w")) { 
                rb.gravityScale = gravity * (fallMiltiplier / 2.5f);
            } 
        }
    }

    private bool isGrounded() {
        RaycastHit2D reycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center,boxCollider2D.bounds.size,0f,Vector2.down,.1f,groundLayerMask);
        return reycastHit2d.collider != null;
    }

    private void Flip() {
       facingRight = !facingRight;
       transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void AnimateMovement() {
        animator.SetFloat("vertical",Mathf.Abs(rb.velocity.y));

        if(isGrounded() && Input.GetButton("Horizontal")) {
            animator.Play("running");
        }else if(isGrounded() && !Input.GetButton("Horizontal")) {
            animator.Play("idle" );
        }
    }
}

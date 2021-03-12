using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [Header("Running")]
    [SerializeField] private float movementSpeed = 50f;
    [SerializeField] private float linearDrag = 12f;
    [SerializeField] private float maxSpeed = 10f; // limiting the addForce.
    private float horizontalDirection;
    private bool facingRight = true;
    private bool changingDirection => (horizontalDirection > 0 && !facingRight) || (horizontalDirection < 0 && facingRight);

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 12f; 
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMiltiplier = 5f;
    [SerializeField] private float lowJumpMultiplier = 3f;
    private float jumpDelay = 0.25f;
    private float jumpTimer; 

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayerMask;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private bool isOnFalling = false;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update() {
        horizontalDirection = getInput().x;
        if(Input.GetKeyDown("w")) {
            jumpTimer = Time.time + jumpDelay;
        }
    }

    void FixedUpdate() {
        MoveCharacter(horizontalDirection);

        if(isGrounded()) {
            ApplyGroundLinearDrag();
        }else {
            ApplyAirLinearDrag();
        }

        if(jumpTimer > Time.time && isGrounded()) {
            Jump();
        }
        modifyJump();
    }

    private Vector2 getInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
    }
    
    private void MoveCharacter(float horizontalDirection) {
        rb.AddForce(Vector2.right * horizontalDirection * movementSpeed);
        AnimateMovement();
        
        if(Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed,rb.velocity.y);
        }
        
        if(changingDirection) {
            Flip();
        }
    }

    private void ApplyGroundLinearDrag() {
        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection) {
            rb.drag = linearDrag;
        }else {
            rb.drag = 0;
        }
    }

    private void ApplyAirLinearDrag() {
        rb.drag = airLinearDrag;
    }

    private void Jump() {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    private void modifyJump() {
        if(isOnFalling) {
            rb.gravityScale = fallMiltiplier + 2f;
        } else {
            if(rb.velocity.y < 0) {
                rb.gravityScale = fallMiltiplier;
            }
            else if(rb.velocity.y > 0 && !Input.GetKeyDown("w")) {
                rb.gravityScale = lowJumpMultiplier;
            } 
            else {
            rb.gravityScale = 1.25f;
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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("movingP")){
            this.transform.SetParent(other.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("movingP")){
            this.transform.SetParent(null);
        }
    }
}

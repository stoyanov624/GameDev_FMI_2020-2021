using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField]
    private Animator animator;

    [Header("Movement")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float linearDrag;
    
    private Vector2 horizontalDirection;
    private Rigidbody2D rb;
    private bool facingRight = true;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontalDirection = new Vector2(Input.GetAxis("Horizontal"),0);
    }

    void FixedUpdate() {
        MoveCharacter(horizontalDirection.x);
        modifyPhysics();
    }

    void MoveCharacter(float horizontalDirection) {
        rb.AddForce(Vector2.right * horizontalDirection * movementSpeed);
        animator.SetFloat("horizontal",Mathf.Abs(rb.velocity.x));
        
        if((horizontalDirection > 0 && !facingRight) || (horizontalDirection < 0 && facingRight))
            Flip();

        if(Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed,rb.velocity.y);
        }
    }

    void modifyPhysics() {
        bool changingDirection = (horizontalDirection.x > 0 && rb.velocity.x < 0) || (horizontalDirection.x < 0 && rb.velocity.x > 0);
        if(Mathf.Abs(horizontalDirection.x) < (linearDrag / 10.0) || changingDirection) {
            rb.drag = linearDrag;
        } else {
            rb.drag = 0;
        }
    }

    void Flip() {
       facingRight = !facingRight;
       transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
}

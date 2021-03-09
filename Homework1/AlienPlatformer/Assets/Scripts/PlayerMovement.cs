using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float movementSpeed = 10f;

    private Vector2 direction;
    private Rigidbody2D rb;
    private bool facingRight = true;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        direction = new Vector2(Input.GetAxis("Horizontal"),0);
    }

    void FixedUpdate() {
        MoveCharacter(direction);
    }

    void MoveCharacter(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * movementSpeed * Time.deltaTime));
        animator.SetFloat("horizontal",Mathf.Abs(direction.x));
        
        if((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
            Flip();
    }

    void Flip() {
       facingRight = !facingRight;
       transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
}

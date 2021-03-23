using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : MonoBehaviour {

    
    [SerializeField] internal PlayerMovement playerScript;
    [SerializeField] internal PlayerCollision collisionScript;

    [Header("Running")]
    [SerializeField] private float movementSpeed = 16f;
    [SerializeField] private float linearDrag = 10f;
    [SerializeField] private float maxSpeed = 8f; // limiting the addForce.
    private float horizontalDirection;
    private bool facingRight = true;
    private bool changingDirection => (horizontalDirection > 0 && !facingRight) || (horizontalDirection < 0 && facingRight);

    void Start() {

    }

    void Update() {
        horizontalDirection = getInput().x;
    }

    void FixedUpdate() {
        MoveCharacter(horizontalDirection);
    }

    private void MoveCharacter(float horizontalDirection) {
        playerScript.rb.AddForce(Vector2.right * horizontalDirection * movementSpeed);
        
        if(Mathf.Abs(playerScript.rb.velocity.x) > maxSpeed) {
            playerScript.rb.velocity = new Vector2(Mathf.Sign(playerScript.rb.velocity.x) * maxSpeed,playerScript.rb.velocity.y);
        }
        
        if(changingDirection) {
            Flip();
        }
    }

    internal void ApplyGroundLinearDrag() {
        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection) {
            playerScript.rb.drag = linearDrag;
        }else {
            playerScript.rb.drag = 0;
        }
    }

    
    private void Flip() {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private Vector2 getInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
    }
}

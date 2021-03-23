using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    [SerializeField] internal PlayerMovement playerScript;
    [SerializeField] internal PlayerJump jumpingScript;

    [SerializeField] private LayerMask groundLayerMask;
    private Coroutine trampolineCoroutine;
    internal bool isOnFalling = false;
    internal bool isOnGround;
    
    void Start() {

    }

    void FixedUpdate() {
        isOnGround = isGrounded();
    }

    internal bool isGrounded() {
        RaycastHit2D reycastHit2d = Physics2D.BoxCast(playerScript.boxCollider2D.bounds.center,playerScript.boxCollider2D.bounds.size,0f,Vector2.down,.1f,groundLayerMask);
        return reycastHit2d.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("trampoline")) {
            trampolineCoroutine = StartCoroutine(Boost());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("trampoline")) {
            StopCoroutine(trampolineCoroutine);
        }
    }

    private IEnumerator Boost() {
        yield return new WaitForSeconds(1f); 
        playerScript.rb.AddForce(Vector2.up * jumpingScript.jumpForce * 2f, ForceMode2D.Impulse);
        yield return 0;
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

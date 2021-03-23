using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    [SerializeField] internal PlayerScript playerScript;
    [SerializeField] internal PlayerJump jumpingScript;

    [SerializeField] private LayerMask groundLayerMask;
    
    [Header("Components")]
    private BoxCollider2D boxCollider2D;
    private Coroutine trampolineCoroutine;
    internal bool isOnFalling = false;
    internal bool isOnGround;
    
    void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate() {
        isOnGround = isGrounded();
    }

    private bool isGrounded() {
        RaycastHit2D reycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center,boxCollider2D.bounds.size,0f,Vector2.down,.1f,groundLayerMask);
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
        yield return new WaitForSeconds(0.55f); 
        playerScript.playerRb.AddForce(Vector2.up * jumpingScript.jumpForce * 2f, ForceMode2D.Impulse);
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

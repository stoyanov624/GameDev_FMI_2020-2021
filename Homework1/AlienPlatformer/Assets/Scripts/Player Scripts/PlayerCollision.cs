using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public static Func<bool> isGroundedCheckDelegate;
    public static Action onDamageTakenDelegate;

    [SerializeField] private LayerMask groundLayerMask;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D playerRB;

    private Coroutine trampolineCoroutine;

    void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }   

    private void OnEnable() {
        isGroundedCheckDelegate += isGrounded;
    }

    private void OnDisable() {
        isGroundedCheckDelegate -= isGrounded;
    }

    private bool isGrounded() {
        RaycastHit2D reycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center,boxCollider2D.bounds.size,0f,Vector2.down,.1f,groundLayerMask);
        return reycastHit2d.collider != null;
    }

    private IEnumerator Boost() {
        yield return new WaitForSeconds(0.55f); 
        playerRB.AddForce(Vector2.up * 30f, ForceMode2D.Impulse);
        SoundManager.instance.PlaySound("trampolineSound");
        yield return 0;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("trampoline")){
            trampolineCoroutine = StartCoroutine(Boost());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("trampoline")){
            StopCoroutine(trampolineCoroutine);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("movingP")){
            this.transform.SetParent(other.transform);
        }

        if(other.gameObject.CompareTag("spikes") || other.gameObject.CompareTag("enemy")) {
            SoundManager.instance.PlaySound("hurtSound");
            onDamageTakenDelegate?.Invoke();
            playerRB.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("movingP")){
            this.transform.SetParent(null);
        }
    }
}

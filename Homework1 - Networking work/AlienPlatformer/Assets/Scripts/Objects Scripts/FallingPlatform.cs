using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
   
    [SerializeField] private float fallDelay = 0.5f;
    private Rigidbody2D rb;
    private bool hasNotBeenTouched = true;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.isKinematic = true; 
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Player") && hasNotBeenTouched) {
            hasNotBeenTouched = false;
            LevelManager.instance.StartCoroutine(LevelManager.instance.RespawnPlatform(new Vector2(transform.position.x,transform.position.y),gameObject.tag, gameObject));
            StartCoroutine(Fall());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 8) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private IEnumerator Fall() {
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
        yield return 0;
    }
}

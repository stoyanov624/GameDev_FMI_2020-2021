using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
   
    [SerializeField] private float fallDelay;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Player")){
            StartCoroutine(Fall());
            Destroy(gameObject, 10);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
   
    private Rigidbody2D rb;
    public float fallDelay;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.CompareTag("Player")){
            StartCoroutine(Fall());
            Destroy(gameObject, 10);
        }
    }

    IEnumerator Fall() {
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
        yield return 0;
    }
}

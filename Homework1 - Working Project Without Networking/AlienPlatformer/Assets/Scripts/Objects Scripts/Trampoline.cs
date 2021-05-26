using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {
    
    [Header("Components")]
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Player")) {
            animator.SetBool("isStepped", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Player")) {
             animator.SetBool("isStepped", false);
        }
    }
}

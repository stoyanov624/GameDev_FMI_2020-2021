using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Player")) {
            Objectives.onKeyPicked?.Invoke();
            Destroy(gameObject);
        }
    }
}

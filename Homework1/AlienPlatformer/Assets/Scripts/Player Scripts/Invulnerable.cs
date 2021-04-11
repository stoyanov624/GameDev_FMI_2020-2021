﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : MonoBehaviour {
    [SerializeField] private Renderer rend;
    private Color color;

    private void Start() {
       color = rend.material.color;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("spikes") || col.gameObject.CompareTag("enemy")) {
            Debug.Log("hurt");
            StartCoroutine("GetInvulnerable");
        }
    }

    private IEnumerator GetInvulnerable() {
        Physics2D.IgnoreLayerCollision(9,10,true);
        Physics2D.IgnoreLayerCollision(9,11,true);
        color = Color.red;
        color.a = 0.5f;
        rend.material.color = color;
        yield return new WaitForSeconds(3f);

        Physics2D.IgnoreLayerCollision(9,10,false);
        Physics2D.IgnoreLayerCollision(9,11,false);
        color = Color.white;
        color.a = 1f;
        rend.material.color = color;
    }
}

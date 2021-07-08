using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : MonoBehaviour {
    [SerializeField] private Renderer rend;
    private Color color;

    private void Awake() {
       Physics2D.IgnoreLayerCollision(9,10,false);
       Physics2D.IgnoreLayerCollision(9,11,false);
    }

    private void Start() {
        color = rend.material.color;
    }

    private void OnEnable() {
        PlayerCollision.onDamageTakenDelegate += GetInvulnerabillity;
    }

    private void OnDisable() {
         PlayerCollision.onDamageTakenDelegate -= GetInvulnerabillity;
    }

    private void GetInvulnerabillity() {
        StartCoroutine("GetInvulnerable");
    }

    private IEnumerator GetInvulnerable() {
        Physics2D.IgnoreLayerCollision(9,10,true);
        Physics2D.IgnoreLayerCollision(9,11,true);
        color.a = 0.5f;
        rend.material.color = color;
        yield return new WaitForSeconds(3f);

        Physics2D.IgnoreLayerCollision(9,10,false);
        Physics2D.IgnoreLayerCollision(9,11,false);
        color.a = 1f;
        rend.material.color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
    private bool isGrounded;
    private bool isMoveKeyPressed;
    [SerializeField] Animator animator;
    
    private void Update() {
        isMoveKeyPressed = Input.GetButton("Horizontal");
        AnimateMovement();
    }

    private void OnEnable() {
        PlayerCollision.onDamageTakenDelegate += HurtAnimation;
    }

    private void OnDisable() {
        PlayerCollision.onDamageTakenDelegate -= HurtAnimation;
    }

    private void FixedUpdate() {
        isGrounded = (bool) PlayerCollision.isGroundedCheckDelegate?.Invoke();        
    }

    private void AnimateMovement() {
        if(isGrounded) {
            animator.SetBool("grounded",true);
            animator.SetBool("moving",isMoveKeyPressed);
        }else {
            animator.SetBool("grounded",false);
        }
    }

    private void HurtAnimation() {
        animator.Play("hurt");
    }
}

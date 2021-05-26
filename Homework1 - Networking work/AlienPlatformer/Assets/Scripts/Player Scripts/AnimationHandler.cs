using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class AnimationHandler : NetworkBehaviour {
    private bool isGrounded;
    private bool isMoveKeyPressed;
    [SerializeField] Animator animator;
    
    private void Update() {
        if(IsOwner) {
            isMoveKeyPressed = Input.GetAxisRaw("Horizontal") != 0;
            AnimateMovement();
        }
    }

    private void OnEnable() {
        LaserShooter.onShootingAction += ShootAnimation;
        PlayerCollision.onDamageTakenDelegate += HurtAnimation;
    }

    private void OnDisable() {
        LaserShooter.onShootingAction -= ShootAnimation;
        PlayerCollision.onDamageTakenDelegate -= HurtAnimation;
    }

    private void FixedUpdate() {
        if(IsOwner) {
            isGrounded = PlayerCollision.isGroundedCheckDelegate;  
        }
    }

    private void AnimateMovement() {
        animator.SetBool("grounded",isGrounded);
        if(isGrounded) {
            animator.SetBool("moving",isMoveKeyPressed);
        }
    }

    private void HurtAnimation() {
        if(IsOwner) {
            animator.SetTrigger("hurt");
        }
    }

    private void ShootAnimation() {
        if(IsOwner) {
            animator.SetTrigger("shoot");
        }
    }
}

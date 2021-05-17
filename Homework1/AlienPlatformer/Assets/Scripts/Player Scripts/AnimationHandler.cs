using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
    private bool isGrounded;
    private bool isMoveKeyPressed;
    [SerializeField] Animator animator;
    
    private void Update() {
        isMoveKeyPressed = Input.GetAxisRaw("Horizontal") != 0;
        AnimateMovement();
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
        animator.SetTrigger("hurt");
    }

    private void ShootAnimation() {
        animator.SetTrigger("shoot");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

    public static Action OnDoorTouch; 
    private bool isDoorOpen = false;
    [SerializeField] private Animator animator;

    private void OnEnable() {
        Objectives.onLevelComplete += OpenDoor;
    }

    private void OnDisable() {
        Objectives.onLevelComplete -= OpenDoor;
    }

    private void OpenDoor() {
        animator.SetBool("openDoor", true);
        isDoorOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isDoorOpen) {
            OnDoorTouch?.Invoke();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private int lives;
    private int remainingLives;
    public static Action<int> drawHeartsDelegate;
    public static Action<int> onDamageTaken;
    public static Action onPlayerDeath;

    private void OnEnable() {
        PlayerCollision.onDamageTakenDelegate += OnHealthLose;
    }

    private void OnDisable() {
        PlayerCollision.onDamageTakenDelegate -= OnHealthLose;
    }
    
    private void Start() {
        drawHeartsDelegate?.Invoke(lives);
        remainingLives = lives;
    }

    private void Update() {
        if(remainingLives == 0) {
            onPlayerDeath?.Invoke();
        }
    }

    private void OnHealthLose() {
        onDamageTaken?.Invoke(--remainingLives);
    }
}

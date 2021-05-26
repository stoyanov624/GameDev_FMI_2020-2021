using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class Health : NetworkBehaviour {
    [SerializeField] private int lives = 3;
    public int RemainingLives {get; set;}
    public static Action<int> drawHeartsDelegate;
    public static Action<int> onDamageTaken;
    public static Action onPlayerDeath;
    public static Action onLastHeart;

    private void OnEnable() {
        PlayerCollision.onDamageTakenDelegate += OnHealthLose;
    }

    private void OnDisable() {
        PlayerCollision.onDamageTakenDelegate -= OnHealthLose;
    }
    
    private void Awake() {
        if(IsOwner) {
            drawHeartsDelegate?.Invoke(lives);
            RemainingLives = lives;
        }
    }

    private void Update() {
        onPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnHealthLose() {
        if(IsOwner) {
            RemainingLives--;
            onDamageTaken?.Invoke(RemainingLives);
            if(RemainingLives == 1) {
                onLastHeart?.Invoke();
            }
        }
    }
}

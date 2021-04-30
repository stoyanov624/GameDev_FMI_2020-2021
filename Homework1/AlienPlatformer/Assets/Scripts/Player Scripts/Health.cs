using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private int lives = 3;
    public int RemainingLives {get; set;}
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
        RemainingLives = lives;
    }

    private void Update() {
        if(RemainingLives == 0) {
            onPlayerDeath?.Invoke();
        }
    }

    private void OnHealthLose() {
        RemainingLives--;
        onDamageTaken?.Invoke(RemainingLives);
        if(RemainingLives == 1) {
            EffectManager.instance.enabled = true;
        }
    }
}

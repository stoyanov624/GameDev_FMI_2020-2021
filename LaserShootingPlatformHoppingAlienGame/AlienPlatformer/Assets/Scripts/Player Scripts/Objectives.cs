using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour {
    [SerializeField] int keysToPick;
    public static Action<int> drawKeysDelegate;
    public static Action onKeyPicked;
    public static Action onLevelComplete;
    private void OnEnable() {
        onKeyPicked += lessenKeysToPick;
        onKeyPicked += invokeLevelCompleteCheck;
    }
    
    private void OnDisable() {
        onKeyPicked -= lessenKeysToPick;
        onKeyPicked -= invokeLevelCompleteCheck;
    }

    private void Start() {
        drawKeysDelegate?.Invoke(keysToPick);
    }

    private void invokeLevelCompleteCheck() {
        if(keysToPick == 0) {
            SoundManager.instance.PlaySound("openDoorSound");
            onLevelComplete?.Invoke();
        }
    }

    private void lessenKeysToPick() {
        keysToPick--;
    }
}

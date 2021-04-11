using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour {
    [SerializeField] int keysToPick;
    public static Action<int> drawKeysDelegate;
    public static Action onKeyPicked;
    void Start() {
        drawKeysDelegate?.Invoke(keysToPick);
    }
}

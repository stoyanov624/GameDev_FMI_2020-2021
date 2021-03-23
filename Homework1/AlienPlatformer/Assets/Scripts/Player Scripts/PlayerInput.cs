using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    
    [SerializeField] internal PlayerMovement playerScript;

    internal bool isJumping;
    internal bool isRunning;

    void Start() {
        
    }

    void Update() {
        isJumping = Input.GetKeyDown("w");
        isRunning = Input.GetButton("Horizontal");
    }
}

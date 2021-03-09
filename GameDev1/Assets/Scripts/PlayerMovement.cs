using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private Vector3 input;


    void Start() {
       rb = GetComponent<Rigidbody>();
    }
    
    void Update() {
        input = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
    }
    
    void FixedUpdate() {
        rb.AddRelativeForce(speed*input);
    }
}

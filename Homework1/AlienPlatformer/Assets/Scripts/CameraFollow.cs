using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float cameraFollowSpeed = 3f;
    private float smoothSpeed;

    [Header("Limits")]
    [SerializeField] private float rightLimit;
    [SerializeField] private float leftLimit;
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;
    private Transform playerTransform;
    private Rigidbody2D playerRb;

    void Start() {
        playerTransform = player.GetComponent<Transform>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Follow();
    }

    private void Follow() {
        Vector3 followPosition = playerTransform.position + offset;
        followPosition.z = -10;
        smoothSpeed = playerRb.velocity.magnitude > cameraFollowSpeed ? playerRb.velocity.magnitude : cameraFollowSpeed;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, followPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothPosition;    

        LimitCamera();
        
    }

    private void LimitCamera() {
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit,rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit,topLimit),
            transform.position.z
        );
    }
}

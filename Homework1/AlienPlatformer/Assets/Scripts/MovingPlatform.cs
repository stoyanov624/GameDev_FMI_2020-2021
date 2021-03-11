using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    
    [SerializeField]
    private float movingSpeed;
    private Vector2 startPosition;
    private Vector2 endPosition;

    void Start() {
        startPosition = transform.position;
        endPosition = new Vector2(transform.position.x + 3,transform.position.y);
    }
    
    void Update() {
        transform.position = Vector2.Lerp(startPosition,endPosition, Mathf.PingPong(Time.time * movingSpeed,1.0f));        
    }
}

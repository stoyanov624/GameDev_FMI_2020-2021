using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    const string LEFT = "left";
    const string RIGHT = "right";
    private string facingDirection;
    private Vector3 baseScale;

    [SerializeField] Transform player;
    [SerializeField] Transform castPos;
    [SerializeField] float movementSpeed;
    [SerializeField] float agroRange;
    [SerializeField] float distFromObstacle;
    [SerializeField] Animator animator;
    private Rigidbody2D enemyRb;

    void Start() {
        facingDirection = RIGHT;
        baseScale = transform.localScale;
        enemyRb = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);   
        float speedDirection = movementSpeed;
        if(facingDirection == LEFT) {
            speedDirection = -movementSpeed;
        }

        enemyRb.velocity = new Vector2(speedDirection,enemyRb.velocity.y);

        if(isHittingWall() || isNearEdge()) {
            if (facingDirection == LEFT) {
                ChangeFacingDirection(RIGHT);
            }
            else if (facingDirection == RIGHT) {
                ChangeFacingDirection(LEFT);
            }
        }
        
        // if(distanceToPlayer < agroRange) {
        //     AtackPlayer();
        // }else {
        //     StopAtackingPlayer();
        // }
    }

    private void ChangeFacingDirection(string newDirection) {
        Vector3 newScale = baseScale;
        if (newDirection == LEFT) {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT) {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;
        facingDirection = newDirection;
    }

    private bool isHittingWall() {
        bool hittingWall = false;

        float castDist = distFromObstacle;
        if(facingDirection == LEFT) {
            castDist = -distFromObstacle;
        }else {
            castDist = distFromObstacle;
        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position,targetPos,Color.red);
        
        if(Physics2D.Linecast(castPos.position,targetPos, 1 << LayerMask.NameToLayer("Ground"))) {
            hittingWall = true;
        }else {
            hittingWall = false;
        }

        return hittingWall;
    }

    private bool isNearEdge() {
         bool nearEdge = true;

        float castDist = distFromObstacle;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position,targetPos,Color.blue);
        
        if(Physics2D.Linecast(castPos.position,targetPos, 1 << LayerMask.NameToLayer("Ground"))) {
            nearEdge = false;
        }else {
            nearEdge = true;
        }

        return nearEdge;
    }

    private void AtackPlayer()
    {
        // if(transform.position.x < player.position.x) {
        //     enemyRb.velocity = new Vector2(movementSpeed,0);
        //     transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        // }else {
        //     enemyRb.velocity = new Vector2(-movementSpeed,0);
        //     transform.localScale = new Vector2(transform.localScale.x * -1f,transform.localScale.y);
        // }
        // animator.Play("EnemyRunning");
    }

    private void StopAtackingPlayer()
    {
        // enemyRb.velocity = Vector2.zero;
        // animator.Play("EnemyIdle");
    }
}

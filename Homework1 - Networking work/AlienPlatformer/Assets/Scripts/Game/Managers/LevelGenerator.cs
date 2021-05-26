using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    [SerializeField] private List<GameObject> levelParts;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform firstPartPosition;

    private float startingY = 15f;

    private void Awake() {
        InitializeStart();
        if(levelParts.Count > 1) {
            startingY += firstPartPosition.position.y;
            InitializeMiddle();
            InitializeFinish();
        }
    } 

    private void InitializeStart() {
        GameObject.Instantiate(levelParts[0], firstPartPosition.position, Quaternion.identity);
        GameObject.Instantiate(player, firstPartPosition.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
    }  

    private void InitializeMiddle() { // if we make more level parts we can have a Random pick for them
        for (int i = 1; i <= 2; i++) {
            GameObject.Instantiate(levelParts[1], firstPartPosition.position + new Vector3(0, startingY, 0), Quaternion.identity);
            startingY += 15;
        }
    }

    private void InitializeFinish() {
        GameObject.Instantiate(levelParts[2], firstPartPosition.position + new Vector3(0, startingY, 0), Quaternion.identity);
    }
}

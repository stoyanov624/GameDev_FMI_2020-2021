using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
public class MyNetworkManager : NetworkBehaviour {

    [SerializeField] private List<GameObject> levelParts;
    [SerializeField] private Transform firstPartPosition;

    private float startingY = 15f;

    private void Awake() {
        InitializeStartServerRpc();
        if(levelParts.Count > 1) {
            startingY += firstPartPosition.position.y;
            InitializeMiddleServerRpc();
            InitializeFinishServerRpc();
        }
    } 
    
    [ServerRpc]
    private void InitializeStartServerRpc() {
        if(OwnerClientId == 0) {
            GameObject go = GameObject.Instantiate(levelParts[0], firstPartPosition.position, Quaternion.identity);
            go.GetComponent<NetworkObject>().Spawn();
        }
    }  

    [ServerRpc]
    private void InitializeMiddleServerRpc() { // if we make more level parts we can have a Random pick for them
        if(OwnerClientId == 0) {
            for (int i = 1; i <= 2; i++) {
                GameObject go = GameObject.Instantiate(levelParts[1], firstPartPosition.position + new Vector3(0,   startingY, 0), Quaternion.identity);
                go.GetComponent<NetworkObject>().Spawn();
                startingY += 15;
            }
        }
    }

    [ServerRpc]
    private void InitializeFinishServerRpc() {
        if(OwnerClientId == 0) {
            GameObject go = GameObject.Instantiate(levelParts[2], firstPartPosition.position + new Vector3(0, startingY, 0) , Quaternion.identity);
            go.GetComponent<NetworkObject>().Spawn();
        }
    }
}

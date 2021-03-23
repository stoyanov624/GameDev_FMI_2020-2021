using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
    public static LevelManager instance;

    [SerializeField] private Transform playerRespawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CameraFollow cam;

    private void Awake() {
        instance = this;
    }

    public void Respawn() {
        GameObject player = Instantiate(playerPrefab, playerRespawnPoint.position, Quaternion.identity);
        //cam.transform.position = new Vector3(0.3f, -1.79f, -10f);
        cam.setPlayerToFollow(player);
    }
}

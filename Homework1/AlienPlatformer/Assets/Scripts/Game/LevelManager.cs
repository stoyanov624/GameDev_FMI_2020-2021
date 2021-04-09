using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour {
    public static LevelManager instance;

    [SerializeField] private Transform playerRespawnPoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject smallFallingPlatformPrefab;
    [SerializeField] private GameObject largeFallingPlatformPrefab;
    [SerializeField] private CameraFollow cam;

    private void Awake() {
        instance = this;
    }

    public void Respawn() {
        playerTransform.position = playerRespawnPoint.position;
        cam.setPlayerToFollow(playerTransform.gameObject);
    }

    public IEnumerator RespawnPlatform(Vector2 respawnPosition, string platformTag) {
        yield return new WaitForSeconds(2f);
        if(platformTag.Equals("smallFallingP")) {
            Instantiate(smallFallingPlatformPrefab,respawnPosition,Quaternion.identity);
        }
        else if(platformTag.Equals("largeFallingP")) {
            Instantiate(largeFallingPlatformPrefab,respawnPosition,Quaternion.identity);
        }
    }
}

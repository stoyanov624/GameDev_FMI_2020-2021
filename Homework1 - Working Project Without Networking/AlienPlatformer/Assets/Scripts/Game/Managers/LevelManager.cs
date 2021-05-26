using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
    public static LevelManager instance;
    private GameObject fallingPlatformReference;

    private void Awake() {
        instance = this;
    }

    public void Respawn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator RespawnPlatform(Vector2 respawnPosition, string platformTag, GameObject platformObj) {
        yield return new WaitForSeconds(2f);
        fallingPlatformReference = platformObj;
        Instantiate(fallingPlatformReference,respawnPosition,Quaternion.identity);
        Destroy(platformObj);
    }
}

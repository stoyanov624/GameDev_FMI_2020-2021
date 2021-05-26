using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.SceneManagement;
public class MainMenu : MonoBehaviour {


    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HostGame() {
        NetworkManager.Singleton.StartHost();
        NetworkSceneManager.SwitchScene("MultiplayerLevel1");
    }

    public void JoinGame() {
        NetworkManager.Singleton.StartClient();
        //NetworkSceneManager.SwitchScene("MultiplayerLevel1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour {

    [SerializeField] private GameObject panel;

    private void OnEnable() {
        LevelComplete.OnDoorTouch += CompleteLevel;
    }

    private void OnDisable() {
        LevelComplete.OnDoorTouch -= CompleteLevel;
    }

    private void CompleteLevel() {
        SoundManager.instance.StopSound("theme");
        SoundManager.instance.PlaySound("levelCompleteSound");
        panel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnNextClick() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitClick() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

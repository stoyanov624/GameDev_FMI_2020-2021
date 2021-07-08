using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour {
    
    [SerializeField] private GameObject panel;

    private void OnEnable() {
        Health.onPlayerDeath += OnDeath;
    }

    private void OnDisable() {
        Health.onPlayerDeath -= OnDeath;
    }

    private void OnDeath() {
        SoundManager.instance.StopSound("theme");
        SoundManager.instance.PlaySound("gameOverSound");
        StartCoroutine(WaitBeforeMenuPop());
    }

    public void OnRespawnClick() {
        Time.timeScale = 1f;
        LevelManager.instance.Respawn();
    }

    public void OnQuitClick() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private IEnumerator WaitBeforeMenuPop() {
        panel.SetActive(true);
        this.enabled = false;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0.0f;
    }
}

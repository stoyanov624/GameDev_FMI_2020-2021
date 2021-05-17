using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour {
    
    [SerializeField] GameObject panel;

    private void OnEnable() {
        Health.onPlayerDeath += OnDeath;
    }

    private void OnDisable() {
        Health.onPlayerDeath -= OnDeath;
    }

    private void OnDeath() {
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
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.0f;
    }
}

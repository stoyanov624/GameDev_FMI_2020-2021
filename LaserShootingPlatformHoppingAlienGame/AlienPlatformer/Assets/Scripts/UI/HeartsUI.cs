using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartsUI : MonoBehaviour {
    [SerializeField] GameObject fullHeartPrefab;
    [SerializeField] GameObject brokenHeartPrefab;
    private List<GameObject> hearts = new List<GameObject>();

    private void OnEnable() {
        Health.drawHeartsDelegate += DrawHearts;
        Health.onDamageTaken += TakeDamage;
        Health.onPlayerDeath += LoseAllHearts;
    }

    private void OnDisable() {
        Health.drawHeartsDelegate -= DrawHearts;
        Health.onDamageTaken -= TakeDamage;
        Health.onPlayerDeath -= LoseAllHearts;
    }


    private void DrawHearts(int heartsCount) {
        for (int i = 0; i < heartsCount; i++) {
            GameObject heart = Instantiate(fullHeartPrefab,transform.position,Quaternion.identity);
            heart.transform.SetParent(transform);
            heart.transform.localScale = new Vector2(1,1);
            hearts.Add(heart);
        }
    }

    private void LoseAllHearts() {
        int heartsCount = hearts.Count;
        for (int i = 0; i < heartsCount; i++) {
            hearts[i].GetComponent<Image>().sprite = brokenHeartPrefab.GetComponent<Image>().sprite;
        }
    }

    private void TakeDamage(int heartsRemaining) {
       hearts[heartsRemaining].GetComponent<Image>().sprite = brokenHeartPrefab.GetComponent<Image>().sprite;
    }
}

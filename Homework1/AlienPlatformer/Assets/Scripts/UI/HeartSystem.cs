using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartSystem : MonoBehaviour {
    [SerializeField] GameObject fullHeartPrefab;
    [SerializeField] GameObject brokenHeartPrefab;
    private List<GameObject> hearts = new List<GameObject>();

    public void DrawHearts(int heartsCount) {
        for (int i = 0; i < heartsCount; i++) {
            GameObject heart = Instantiate(fullHeartPrefab,transform.position,Quaternion.identity);
            heart.transform.SetParent(transform);
            heart.transform.localScale = new Vector2(1,1);
            hearts.Add(heart);
        }
    }

    private void RestoreHearts() {
        for (int i = 0; i < hearts.Count; i++) {
            hearts[i].GetComponent<Image>().sprite = fullHeartPrefab.GetComponent<Image>().sprite;
        }
    }

    public void TakeDamage(int lastHeart) {
       if(lastHeart == 0) {
           RestoreHearts();
       } 
       else {
            hearts[lastHeart].GetComponent<Image>().sprite = brokenHeartPrefab.GetComponent<Image>().sprite;
       }
    }

    public int getMaxLives() {
        return hearts.Count;
    }
}

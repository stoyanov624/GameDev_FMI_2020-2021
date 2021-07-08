using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeysUI : MonoBehaviour {
    [SerializeField] GameObject missingKeyPrefab;
    [SerializeField] GameObject pickedKeyPrefab;

    private List<GameObject> keys = new List<GameObject>();
    private int keysRemaining;

    private void OnEnable() {
        Objectives.drawKeysDelegate += DrawKeys;
        Objectives.onKeyPicked += PickKey;
        Health.onPlayerDeath += LoseKeys;
    }

    private void OnDisable() {
        Objectives.drawKeysDelegate -= DrawKeys;
        Objectives.onKeyPicked -= PickKey;
        Health.onPlayerDeath -= LoseKeys;
    }

    private void DrawKeys(int keyCount) {
        for (int i = 0; i < keyCount; i++) {
            GameObject key = Instantiate(missingKeyPrefab,transform.position,Quaternion.identity);
            key.transform.SetParent(transform);
            key.transform.localScale = new Vector2(1,1);
            keys.Add(key);
        }
        keysRemaining = keys.Count;
    }

    private void LoseKeys() {
        keysRemaining = keys.Count;
        for (int i = 0; i < keys.Count; i++) {
            keys[i].GetComponent<Image>().sprite = missingKeyPrefab.GetComponent<Image>().sprite;
        }
    }

    private void PickKey() {
        keysRemaining--;
        if(keysRemaining >= 0) {
             keys[keysRemaining].GetComponent<Image>().sprite = pickedKeyPrefab.GetComponent<Image>().sprite;
        }
    }
}

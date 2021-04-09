using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeySystem : MonoBehaviour {
    [SerializeField] GameObject missingKeyPrefab;
    [SerializeField] GameObject pickedKeyPrefab;

    private List<GameObject> keys = new List<GameObject>();

    public void DrawKeys(int keyCount) {
        for (int i = 0; i < keyCount; i++) {
            GameObject key = Instantiate(missingKeyPrefab,transform.position,Quaternion.identity);
            key.transform.SetParent(transform);
            key.transform.localScale = new Vector2(1,1);
            keys.Add(key);
        }
    }

    public int getMaxKeys() {
        return keys.Count;
    }

    public void LoseKeys() {
        for (int i = 0; i < keys.Count; i++) {
            keys[i].GetComponent<Image>().sprite = missingKeyPrefab.GetComponent<Image>().sprite;
        }
    }

    public void PickKey(int lastKey) {
        if(lastKey >= 0) {
             keys[lastKey].GetComponent<Image>().sprite = pickedKeyPrefab.GetComponent<Image>().sprite;
        }
    }
}

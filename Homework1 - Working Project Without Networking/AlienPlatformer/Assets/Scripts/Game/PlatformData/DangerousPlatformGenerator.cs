using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousPlatformGenerator : MonoBehaviour {
    [SerializeField] private GameObject largePlatform;
    [SerializeField] private List<GameObject> dangers;
    [SerializeField] private Transform dangerSpawnPos;

    private void Awake() {
        int dangersCount = dangers.Count;
        SpawnPlatform();    
        SpawnDanger(dangersCount);
    }

    private void SpawnPlatform() {
        GameObject.Instantiate(largePlatform, transform.position, Quaternion.identity);
    }

    private void SpawnDanger(int dangersCount) {
        int randomDangerIndex = Random.Range(0, dangersCount);
        Vector2 randomDangerSpawnPosition = new Vector2(Random.Range(dangerSpawnPos.position.x - 0.8f, 
                                                                dangerSpawnPos.position.x + 0.8f), 
                                                                dangerSpawnPos.position.y);

        GameObject.Instantiate(dangers[randomDangerIndex], randomDangerSpawnPosition, Quaternion.identity);
    }

}

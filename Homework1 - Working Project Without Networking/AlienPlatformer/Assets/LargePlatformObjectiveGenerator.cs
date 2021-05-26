using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePlatformObjectiveGenerator : MonoBehaviour {
    [SerializeField] private GameObject objectivePrefab;

    [SerializeField] private Transform objectiveMaxRightPosition;
    [SerializeField] private Transform objectiveMaxLeftPosition;

    private GameObject objective;
    private LargePlatformGenerator platformGenerator;

    private void Start() {
        platformGenerator = GetComponent<LargePlatformGenerator>();
        SpawnObjective();
    }

    private void SpawnObjective() {
        Vector2 randomKeySpawnPosition = new Vector2(Random.Range(objectiveMaxLeftPosition.position.x, 
                                                                objectiveMaxRightPosition.position.x), 
                                                                objectiveMaxRightPosition.position.y);

        objective = GameObject.Instantiate(objectivePrefab, randomKeySpawnPosition, Quaternion.identity);
        if(objective.CompareTag("trampoline")) {
            objective.transform.SetParent(platformGenerator.Platform.transform);
        }
    }

}

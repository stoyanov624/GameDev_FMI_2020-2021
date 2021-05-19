using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePlatformGenerator : MonoBehaviour {
    [SerializeField] private List<GameObject> platformTypes;
    [SerializeField] private List<GameObject> decorations;
    [SerializeField] private Transform leftDecorationPos;
    [SerializeField] private Transform rightDecorationPos;
    
    private GameObject leftDecor;
    private GameObject rightDecor;
    private GameObject platform;

    private void Awake() {
        int decorationsCount = decorations.Count;
        int platformTypesCount = platformTypes.Count;
        
        GenerateLeftDecoration(decorationsCount);
        GenerateRightDecoration(decorationsCount);
        GeneratePlatformType(platformTypesCount);
    }
   

    private void GenerateRightDecoration(int decorationCounts) {
        int randomDecoration = Random.Range(0, decorations.Count - 1);
        float randomRightPosX = Random.Range(rightDecorationPos.position.x - 0.5f, rightDecorationPos.position.x);
        rightDecor = GameObject.Instantiate(decorations[randomDecoration], 
                                    new Vector2(randomRightPosX, rightDecorationPos.position.y), 
                                    Quaternion.identity);
    }

    private void GenerateLeftDecoration(int  decorationCounts) {
        int randomDecoration = Random.Range(0, decorations.Count - 1);
        float randomLeftPosX = Random.Range(leftDecorationPos.position.x, leftDecorationPos.position.x + 0.5f);
        leftDecor = GameObject.Instantiate(decorations[randomDecoration], 
                                    new Vector2(randomLeftPosX, leftDecorationPos.position.y),
                                     Quaternion.identity);
    }

    private void GeneratePlatformType(int platformTypesCount) {
        int randomPlatformType = Random.Range(0, platformTypesCount);

        platform = GameObject.Instantiate (platformTypes[randomPlatformType], 
                                    transform.position, Quaternion.identity);

        leftDecor.transform.SetParent(platform.transform);
        rightDecor.transform.SetParent(platform.transform);
    }
}

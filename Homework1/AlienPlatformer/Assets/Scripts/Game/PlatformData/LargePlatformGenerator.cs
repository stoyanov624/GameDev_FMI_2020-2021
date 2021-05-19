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
    public GameObject Platform {get; set;}

    private void Awake() {
        int decorationsCount = decorations.Count;
        int platformTypesCount = platformTypes.Count;

        GeneratePlatformType(platformTypesCount);
        GenerateLeftDecoration(decorationsCount);
        GenerateRightDecoration(decorationsCount);
    }
   

    private void GenerateRightDecoration(int decorationCounts) {
        int randomDecoration = Random.Range(0, decorations.Count - 1);
        float randomRightPosX = Random.Range(rightDecorationPos.position.x - 0.5f, rightDecorationPos.position.x);
        rightDecor = GameObject.Instantiate(decorations[randomDecoration], 
                                    new Vector2(randomRightPosX, rightDecorationPos.position.y), 
                                    Quaternion.identity);
        
        rightDecor.transform.SetParent(Platform.transform);
    }

    private void GenerateLeftDecoration(int  decorationCounts) {
        int randomDecoration = Random.Range(0, decorations.Count - 1);
        float randomLeftPosX = Random.Range(leftDecorationPos.position.x, leftDecorationPos.position.x + 0.5f);
        leftDecor = GameObject.Instantiate(decorations[randomDecoration], 
                                    new Vector2(randomLeftPosX, leftDecorationPos.position.y),
                                     Quaternion.identity);

        leftDecor.transform.SetParent(Platform.transform);
    }

    private void GeneratePlatformType(int platformTypesCount) {
        int randomPlatformType = Random.Range(0, platformTypesCount);

        Platform = GameObject.Instantiate (platformTypes[randomPlatformType], 
                                    transform.position, Quaternion.identity);
        
        Platform.transform.SetParent(transform);
    }
}

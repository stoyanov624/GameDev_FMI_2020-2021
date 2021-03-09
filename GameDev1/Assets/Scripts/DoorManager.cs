using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {

    [SerializeField]
    GameObject door;
    bool shouldOpen = false;

  
    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Player")) {
            shouldOpen = true;
            StartCoroutine(openDoor());
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag.Equals("Player")) {
            shouldOpen = false;
            StartCoroutine(closeDoor());
        }
    }

    IEnumerator openDoor() {
        while(shouldOpen) {
            door.transform.Translate(Time.deltaTime * new Vector3(0,3,0));
            
            if(door.transform.position.y >= 8.51) {
                door.transform.position = new Vector3(door.transform.position.x,8.51f,door.transform.position.z);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator closeDoor() {
        while(!shouldOpen) {
            door.transform.Translate(Time.deltaTime * new Vector3(0,-3,0));

            if(door.transform.position.y <= 4.33) {
                door.transform.position = new Vector3(door.transform.position.x,4.33f,door.transform.position.z);
                yield break;
            }

            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    private Dictionary<string, AudioSource> sounds = new Dictionary<string, AudioSource>();
    [SerializeField] private List<string> keys;
    [SerializeField] private List<AudioSource> values;

    private void Start() {
        instance = this;

        for (int i = 0, keyCount = keys.Count; i < keyCount; i++) {
            sounds.Add(keys[i], values[i]);
        }
    }

    public void PlaySound(string soundName) {
        sounds[soundName].Play();
    }

    public void StopSound(string soundName) {
        sounds[soundName].Stop();
    }
}

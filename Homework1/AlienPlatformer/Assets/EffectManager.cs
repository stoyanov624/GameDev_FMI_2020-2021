using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectManager : MonoBehaviour {
    public PostProcessVolume volume;
    private Vignette vignette;
    private bool hasOneHealth = false;

    private void Start() {
        volume.profile.TryGetSettings(out vignette);
        increaseVigneteIntensity();
    }

    private void Update() {
        
        vignette.intensity.value = Mathf.Lerp(0.4f, 0.55f, Mathf.PingPong(Time.time,1f));   
    }

    private void increaseVigneteIntensity() {
        while(vignette.intensity.value < 0.55f) {
            vignette.intensity.value = Mathf.Lerp(0f, 0.55f, Time.time);
        }
    }
}

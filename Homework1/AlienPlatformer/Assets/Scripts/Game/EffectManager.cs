using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectManager : MonoBehaviour {
    public static EffectManager instance;

    public PostProcessVolume volume;
    private Vignette vignette;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        volume.profile.TryGetSettings(out vignette);
        
        vignette.intensity.value = Mathf.Floor(Time.time);
        
    }

    private void Update() {
        //pumpEffect();
    }

    private void pumpEffect() {
        vignette.intensity.value = Mathf.Lerp(0.4f, 0.55f, Mathf.PingPong(Time.time,1f));
    }
}

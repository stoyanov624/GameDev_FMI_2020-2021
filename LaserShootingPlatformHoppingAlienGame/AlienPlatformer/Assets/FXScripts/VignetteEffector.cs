using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffector : MonoBehaviour {
    private Volume volume;
    private Vignette vignette;
    
    private void OnEnable() {
        Health.onLastHeart += startVignetteEffect;
    }

    private void OnDisable() {
        Health.onLastHeart -= startVignetteEffect;
    }

    private void Start() {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<Vignette>(out vignette);
    }

    private void Update() {
        if (vignette.active) {
            PumpEffect();
        }
    }

    private void PumpEffect() {
        vignette.intensity.value = Mathf.Lerp(0.20f, 0.25f, Mathf.PingPong(Time.time, 1f));
    }

    private void startVignetteEffect() {
        vignette.active = true;
    }
}

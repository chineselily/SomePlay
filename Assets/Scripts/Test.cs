using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TinyTeam.UI;

public class Test : MonoBehaviour {

    private void Start() {

        WebCamDevice[] devices = WebCamTexture.devices;

        WebCamTexture webcamTexture = null;

        if (devices.Length == 0) {
            return;
        }

#if UNITY_EDITOR
        if (devices.Length == 1) {
            webcamTexture = new WebCamTexture(1280, 720,25);
        } else if (devices.Length > 1) {
            webcamTexture = new WebCamTexture("Logitech HD Pro Webcam C920", 1280, 720,25);
        }
#else
        webcamTexture = new WebCamTexture(960, 540,20);
#endif

        Renderer renderer = GetComponent<Renderer>();
        renderer.sharedMaterial.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWebCam : MonoBehaviour {


    private void Start() {

        WebCamDevice[] devices = WebCamTexture.devices;

        WebCamTexture webcamTexture = null;

        if (devices.Length == 0) {
            return;
        }

#if UNITY_EDITOR
        if (devices.Length == 1) {
            webcamTexture = new WebCamTexture(1280, 720);
        } else if (devices.Length > 1) {
            webcamTexture = new WebCamTexture("Logitech HD Pro Webcam C920", 1280, 720);
        }
#else
        webcamTexture = new WebCamTexture(720, 405);
#endif

        Renderer renderer = GetComponent<Renderer>();
        renderer.sharedMaterial.mainTexture = webcamTexture;
        webcamTexture.Play();

        Texture tex = webcamTexture as Texture;
        IBLManager.videoTex = tex as Texture2D;
    }
    
}

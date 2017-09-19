using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using DG.Tweening;

public class CollideBlock : MonoBehaviour {

    public PostProcessingProfile pp_profile;
    private VignetteModel.Settings vignette;
    private Tweener[] tweener;
    private Color color = Color.black;
    private float smoothness = 0.45f;

    private void Start() {
        tweener = new Tweener[2];
        vignette = pp_profile.vignette.settings;
    }

    void OnTriggerEnter (Collider other) {
        //赋值
        color = Color.red;
        smoothness = 0.5f;
        //缓动
        if(tweener[0] != null) {
            for(int i = 0;i < tweener.Length; i++) { 
                tweener[i].Restart();
            }
        } else {
            tweener[0] = DOTween.To(() => color, x => color = x, Color.black, 1);
            tweener[1] = DOTween.To(() => smoothness, x => smoothness = x, 0.2f, 1);
        }

        Handheld.Vibrate();//震动
    }

    private void Update() {
        if(tweener[0] != null) {
            if (tweener[0].IsPlaying() || tweener[1].IsPlaying()) {
                vignette.color = color;
                vignette.smoothness = smoothness;
                pp_profile.vignette.settings = vignette;
            } else {
                for(int i = 0;i < tweener.Length; i++) {
                    if(tweener[i] != null) { 
                        tweener[i].Kill();
                        tweener[i] = null;
                    }
                }
                
            }
        }
    }
}

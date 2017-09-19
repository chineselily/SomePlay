using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IBLManager : MonoBehaviour {

    public static Texture2D videoTex;
    private ReflectionProbe reflector;
    public static Texture refTex;

    private void Start() {
        reflector = this.GetComponent<ReflectionProbe>();
    }

    private void Update() {
        if (reflector != null) {
            if (reflector.IsFinishedRendering(0)) {
                //reflector.texture是一个cube类型的Rendertexture
                refTex = reflector.texture;

                //DynamicGI.UpdateEnvironment();
            }
        }
    }

    //public Camera EnviromentCam;
    ////public GameObject SkyMatObject;
    //public static Texture2D videoTex;
    //public Transform obj;

    //private int cubemapSize = 128;
    //private RenderTexture rtex;
    //private float t = 0;



    //private void Update() {

    //    if (t < 0.3f) {
    //        t += Time.deltaTime;
    //    } else {
    //        t = 0;

    //        int faceToRender = Time.frameCount % 6;
    //        int faceMask = 1 << faceToRender;
    //        RefreshCubemap(faceMask);
    //        //刷新场景GI
    //        //DynamicGI.UpdateEnvironment();
    //    }


    //}


    //void RefreshCubemap(int faceMask) {

    //    if (!rtex) {
    //        rtex = new RenderTexture(cubemapSize, cubemapSize, 16);
    //        rtex.dimension = UnityEngine.Rendering.TextureDimension.Cube;
    //        rtex.hideFlags = HideFlags.HideAndDontSave;
    //        //赋予天空材质，指定cubemap
    //        //SkyMatObject.GetComponent<Renderer>().sharedMaterial.SetTexture("_Tex", rtex);
    //        obj.GetComponent<Renderer>().sharedMaterial.SetTexture("_Cube", rtex);
    //    }


    //    EnviromentCam.RenderToCubemap(rtex, faceMask);
    //}
}

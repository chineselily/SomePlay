using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class DroneMainUI : TTUIPage
{

    public DroneMainUI() : base(UIType.Normal, UIMode.DoNothing, UICollider.None) {
        uiPath = "UIPrefabs/DroneMainUI";
    }
    public override void Awake(GameObject go)
    {
        transform.root.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler cs = transform.root.GetComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(2208f, 1242f);//new Vector2(1136f, 640f);
        cs.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;//CanvasScaler.ScreenMatchMode.Expand;
        cs.matchWidthOrHeight = 1;
    }
}

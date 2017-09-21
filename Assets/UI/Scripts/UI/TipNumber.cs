using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class TipNumber : TTUIPage
{
    public TipNumber() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None) {
        uiPath = "UIPrefabs/TipNumber";
    }
    public override void Awake(GameObject go)
    {
        transform.root.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    }
    public void SetNumber(int num)
    {
        TipNumberImageSetter tseter = gameObject.GetComponent<TipNumberImageSetter>();
        List<Sprite> tlist = num < 0 ? tseter.negativeImages : tseter.positiveImages;
        transform.Find("sign").GetComponent<Image>().sprite = num < 0 ? tseter.negative : tseter.positive;
        num = Mathf.Abs(num);
        string snum = num.ToString();
        //只取第一位显示
        num = int.Parse(snum.Substring(0,1)); 
        transform.Find("number").GetComponent<Image>().sprite = tlist[num];
    }
}

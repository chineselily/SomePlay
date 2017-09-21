using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;

public class TipNumber : TTUIPage
{
    private Vector3 InitPosition;
    public TipNumber() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None) {
        uiPath = "UIPrefabs/TipNumber";
    }
    public override void Awake(GameObject go)
    {
        transform.root.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        InitPosition = go.transform.localPosition;
    }
    public void SetNumber(int num)
    {
        TipNumberImageSetter tseter = gameObject.GetComponent<TipNumberImageSetter>();
        List<Sprite> tlist = num < 0 ? tseter.negativeImages : tseter.positiveImages;

        DoTween(num > 0);

        transform.Find("sign").GetComponent<Image>().sprite = num < 0 ? tseter.negative : tseter.positive;
        num = Mathf.Abs(num);
        string snum = num.ToString();
        //只取第一位显示
        num = int.Parse(snum.Substring(0,1)); 
        transform.Find("number").GetComponent<Image>().sprite = tlist[num];
    }

    private void DoTween(bool bUp)
    {
        transform.localPosition = InitPosition;

        Sequence mySequence = DOTween.Sequence();
        float ty = bUp ? 60 : -60;

        mySequence.Append(transform.DOLocalMoveY(InitPosition.y + ty, 1)).AppendInterval(0.5f);
        mySequence.OnComplete(DoTweenEnd);
    }

    void DoTweenEnd()
    {
        UITipHelper.CloseTip<TipNumber>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;

public class TipNumber : TTUIPage
{
    private Vector3 InitPosition = Vector3.zero;
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
        int anum = Mathf.Abs(num);
        string snum = anum.ToString();
        //只取第一位显示
        anum = int.Parse(snum.Substring(0,1)); 
        transform.Find("number").GetComponent<Image>().sprite = tlist[anum];

        DoTween(num > 0);
    }

    private void DoTween(bool bUp)
    {
        //Debug.Log("DoTween-bUp=" + bUp + "-InitPosition" + InitPosition);
        //transform.localPosition = InitPosition;
        transform.GetComponent<CanvasGroup>().alpha = 1;

        Sequence mySequence = DOTween.Sequence();
        float ty = bUp ? transform.localPosition.y+150 : transform.localPosition.y-150;

        mySequence.Append(transform.DOLocalMoveY(ty, 1)).Append(transform.GetComponent<CanvasGroup>().DOFade(0, 1));
        //mySequence.AppendInterval(1);
        mySequence.OnComplete(DoTweenEnd);
    }

    void DoTweenEnd()
    {
        transform.localPosition = InitPosition;
        UITipHelper.CloseTip<TipNumber>();
    }
}

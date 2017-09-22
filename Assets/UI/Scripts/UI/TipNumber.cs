﻿using System.Collections;
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
        transform.localPosition = InitPosition;

        Sequence mySequence = DOTween.Sequence();
        float ty = bUp ? 60 : -60;

        mySequence.Append(transform.DOLocalMoveY(InitPosition.y + ty, 1)).AppendInterval(0.5f);
        //mySequence.AppendInterval(1);
        mySequence.OnComplete(DoTweenEnd);
    }

    void DoTweenEnd()
    {
        transform.localPosition = InitPosition;
        UITipHelper.CloseTip<TipNumber>();
    }
}

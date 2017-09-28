using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapScroll : MonoBehaviour {

    public List<Transform> maps = new List<Transform>();
    List<float> mXPosition = new List<float>() { 400, 320, 480 };
    List<float> mScale = new List<float> { 0.55f, 0.75f, 1f};
    int mXThreshold = 50;//x轴滑动超过50像素，则滚动
    Vector2 mstartPoint;
    bool mInAnimation = false;//是否在动画中

    private void Start()
    {

    }
    public void OnBeginDragMsg(Vector2 start)
    {
        mstartPoint = start;
    }
    public void OnDragMsg(Vector2 current)
    {
        if (mInAnimation) return;
        float difx = current.x - mstartPoint.x;
        if (Mathf.Abs(difx) < mXThreshold) return;
        StartAnimation(difx < 0);
    }
    void StartAnimation(bool bLeft)
    {
        mInAnimation = true;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(maps[0].DOLocalMoveX(maps[0].transform.localPosition.x - 400,1));

        mySequence.Join(maps[1].DOLocalMoveX(maps[1].transform.localPosition.x-320,1));
        mySequence.Join(maps[1].DOScaleX(0.55f, 1));
        mySequence.Join(maps[1].DOScaleY(0.55f, 1));

        mySequence.Join(maps[2].DOLocalMoveX(maps[2].transform.localPosition.x - 480, 1));
        mySequence.Join(maps[2].DOScaleX(0.75f, 1));
        mySequence.Join(maps[2].DOScaleY(0.75f, 1));

        mySequence.Join(maps[3].DOLocalMoveX(maps[3].transform.localPosition.x - 480, 1));
        mySequence.Join(maps[3].DOScaleX(1f, 1));
        mySequence.Join(maps[3].DOScaleY(1f, 1));

        maps[3].DOLocalMoveX(maps[3].transform.localPosition.x - 480, 1);

        mySequence.Join(maps[4].DOLocalMoveX(maps[4].transform.localPosition.x - 320, 1));
        mySequence.Join(maps[4].DOScaleX(0.75f, 1));
        mySequence.Join(maps[4].DOScaleY(0.75f, 1));
    }
}

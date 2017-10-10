using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollToNeighborSecond : MonoBehaviour {

    public ScrollDirection direction = ScrollDirection.horizontal;
    public int threshold = 50;//超过50像素，则滚动
    public List<GameObject> scrollItems = new List<GameObject>();//参与滚动的物体，按照滚动的方向排序

    Vector2 mstartPoint;
    bool mInAnimation = false;//是否在动画中
    ScrollToNeighborParam myParam;
    ItemScrollToNeighborDatas mDatas;

    private void Awake()
    {
        mDatas = GetComponent<ItemScrollToNeighborDatas>();

        myParam = new ScrollToNeighborParam();
        myParam.duration = 0.5f;
        myParam.InitScrollItems(scrollItems, null, null);
        myParam.datas = mDatas;
    }
    private void Start()
    {
        if (null != mDatas) mDatas.UpdateData();
    }
    public void OnBeginDragMsg(Vector2 start)
    {
        mstartPoint = start;
    }
    public void OnDragMsg(Vector2 current)
    {
        if (mInAnimation)
        {
            mstartPoint = current;
            return;
        }

        float difx = direction== ScrollDirection.horizontal?current.x - mstartPoint.x:current.y-mstartPoint.y;
        if (Mathf.Abs(difx) < threshold) return;
        myParam.bSameAsBeforeItem = difx < 0 ? true : false;
        if (myParam.bSameAsBeforeItem && null != mDatas && !mDatas.canScrollToBefore) return;
        if (!myParam.bSameAsBeforeItem && null != mDatas && !mDatas.canScrollToAfter) return;

        mInAnimation = true;
        if (null != mDatas) mDatas.Scroll(myParam.bSameAsBeforeItem);
        myParam.sequece = DOTween.Sequence();
        BroadcastMessage("ScrollItem", myParam, SendMessageOptions.DontRequireReceiver);
        myParam.sequece.OnComplete(onTweenComplete);
    }

    void onTweenComplete()
    {
        if (null != mDatas)
        {
            //mDatas.Scroll(myParam.bSameAsBeforeItem);
            mDatas.UpdateData();
        }
        BroadcastMessage("ScrollItemEnd", myParam, SendMessageOptions.DontRequireReceiver);

        mInAnimation = false;
    }
}

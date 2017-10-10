using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScrollDirection { horizontal, vertical}
public class ScrollToNeighbor : MonoBehaviour {

    public ScrollDirection direction = ScrollDirection.horizontal;
    public int threshold = 50;//超过50像素，则滚动
    public GameObject beforeOutMaskItem;//向前滚出参照物，可以不设置，如果不设置，则最前的物体不滚出
    public GameObject afterOutMaskItem;//向后滚出参照物，可以不设置，如果不设置，则最后的物体不滚出
    public List<GameObject> scrollItems = new List<GameObject>();//参与滚动的物体，按照滚动的方向排序
    public int minItemNum = 3;//最少应该显示3个item
    public bool bCycle = false;//循环滚动

    Vector2 mstartPoint;
    bool mInAnimation = false;//是否在动画中
    ScrollToNeighborParam myParam;

    private void Awake()
    {
        myParam = new ScrollToNeighborParam();
        myParam.bCycle = bCycle;
        myParam.duration = 0.5f;
        myParam.InitScrollItems(scrollItems, beforeOutMaskItem, afterOutMaskItem);
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
        if (!myParam.canScroll(minItemNum)) return;
        mInAnimation = true;
        myParam.sequece = DOTween.Sequence();
        myParam.UpdateCurScrollItems();
        BroadcastMessage("ScrollItem", myParam, SendMessageOptions.DontRequireReceiver);
        myParam.sequece.OnComplete(onTweenComplete);
    }

    void onTweenComplete()
    {
        mInAnimation = false;
    }
}

public class ScrollToNeighborParam
{
    public bool bCycle;
    public bool bSameAsBeforeItem;
    public float duration;
    public Sequence sequece;
    public ItemScrollToNeighborDatas datas;
    public List<GameObject> curScrollItems;
    public List<Vector3> curScrollPositions;
    public List<Vector3> curScrollLocalScales;

    public OutMaskRefereceInfo beforeOutMaskInfo;
    public OutMaskRefereceInfo afterOutMaskInfo;
    public List<Vector3> initScrollItemsLocalPosition;
    public List<Vector3> initScrollItemsLocalScale;
    public List<int> initScrollItemsSiblingIndex;
    public List<GameObject> initItems;

    public List<GameObject> visibleItems;
    public List<GameObject> beforeItems;
    public List<GameObject> afterItems;
    public void InitScrollItems(List<GameObject> tlist, GameObject beforeReference, GameObject afterReference)
    {
        if (null == tlist) return;
        if(null != beforeReference)
        {
            beforeOutMaskInfo = new OutMaskRefereceInfo();
            beforeOutMaskInfo.InitOutMaskInfo(beforeReference);
        }
        if(null != afterReference)
        {
            afterOutMaskInfo = new OutMaskRefereceInfo();
            afterOutMaskInfo.InitOutMaskInfo(afterReference);
        }

        curScrollItems = new List<GameObject>();
        beforeItems = new List<GameObject>();
        afterItems = new List<GameObject>();
        curScrollLocalScales = new List<Vector3>();
        curScrollPositions = new List<Vector3>();

        visibleItems = new List<GameObject>(tlist);
        initItems = new List<GameObject>(tlist);
        initScrollItemsSiblingIndex = new List<int>();
        initScrollItemsLocalScale = new List<Vector3>();
        initScrollItemsLocalPosition = new List<Vector3>();

        for(int i=0;i< visibleItems.Count;i+=1)
        {
            initScrollItemsSiblingIndex.Add(visibleItems[i].transform.GetSiblingIndex());
            initScrollItemsLocalPosition.Add(visibleItems[i].transform.localPosition);
            initScrollItemsLocalScale.Add(visibleItems[i].transform.localScale);
        }
    }
    public bool havaData(GameObject titem)
    {
        if (null == datas) return true;
        return datas.haveData(titem);
    }
    public bool canScroll(int minItemNum)
    {
        int curNum = 0;
        for(int i=0;i<visibleItems.Count;i+=1)
        {
            curNum += visibleItems[i] != null ? 1 : 0;
        }
        if (curNum > minItemNum) return true;
        if (bSameAsBeforeItem && visibleItems[0] == null) return true;
        if (!bSameAsBeforeItem && visibleItems[visibleItems.Count - 1] == null) return true;
        return false;
    }
    
    public void UpdateCurScrollItems()
    {
        curScrollItems.Clear();
        curScrollLocalScales.Clear();
        curScrollPositions.Clear();

        int removeindex = bSameAsBeforeItem ? 0 : visibleItems.Count - 1;
        GameObject removeone = visibleItems[removeindex];
        List<GameObject> poplist = bSameAsBeforeItem ? afterItems : beforeItems;
        List<GameObject> addlist = bSameAsBeforeItem ? beforeItems : afterItems;
        GameObject addone = poplist.Count > 0 ? poplist[poplist.Count - 1] : null;

        visibleItems.RemoveAt(removeindex);
        int insertindex = bSameAsBeforeItem ? visibleItems.Count : 0;
        visibleItems.Insert(insertindex, addone);

        if (poplist.Count > 0) poplist.RemoveAt(poplist.Count - 1);
        if (null != removeone)
        {
            Vector3 rposition; Vector3 rscale;
            addlist.Add(removeone);
            curScrollItems.Add(removeone);
            if (bSameAsBeforeItem)
            {
                rposition = beforeOutMaskInfo != null ? beforeOutMaskInfo.outMaskItemLocalPosition : initScrollItemsLocalPosition[0];
                rscale = beforeOutMaskInfo != null ? beforeOutMaskInfo.outMaskItemLocalScale : initScrollItemsLocalScale[0];
            }
            else
            {
                rposition = afterOutMaskInfo != null ? afterOutMaskInfo.outMaskItemLocalPosition : initScrollItemsLocalPosition[initScrollItemsLocalPosition.Count - 1];
                rscale = afterOutMaskInfo != null ? afterOutMaskInfo.outMaskItemLocalScale : initScrollItemsLocalScale[initScrollItemsLocalScale.Count - 1];
            }
            curScrollPositions.Add(rposition);
            curScrollLocalScales.Add(rscale);
        }

        for(int i=0;i<visibleItems.Count;i+=1)
        {
            if(null != visibleItems[i])
            {
                curScrollItems.Add(visibleItems[i]);
                curScrollPositions.Add(initScrollItemsLocalPosition[i]);
                curScrollLocalScales.Add(initScrollItemsLocalScale[i]);
            }
        }

        for (int i = 0; i < visibleItems.Count; i += 1)
        {
            if(null != visibleItems[i])
                visibleItems[i].transform.SetSiblingIndex(initScrollItemsSiblingIndex[i]);
        }
        for (int i=0; i<beforeItems.Count;i+=1)
        {
            beforeItems[i].transform.SetAsFirstSibling();
        }
        for(int i=0;i<afterItems.Count;i+=1)
        {
            afterItems[i].transform.SetAsFirstSibling();
        }
    }
}

public class OutMaskRefereceInfo
{
    public Vector3 outMaskItemLocalPosition;
    public Vector3 outMaskItemLocalScale;
    public int outMaskItemSiblingIndex;

    public void InitOutMaskInfo(GameObject go)
    {
        if (null == go) return;
        outMaskItemLocalPosition = go.transform.localPosition;
        outMaskItemLocalScale = go.transform.localScale;
        outMaskItemSiblingIndex = go.transform.GetSiblingIndex();
    }
}

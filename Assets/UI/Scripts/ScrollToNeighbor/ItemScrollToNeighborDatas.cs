using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScrollToNeighborDatas : MonoBehaviour {

    public List<Sprite> scrollInitDatas = new List<Sprite>();//参与显示的数据
    private int middleIndex = 0;//中间显示数据

    public List<GameObject> scrollItems = new List<GameObject>();//参与显示的Item

    private void Awake()
    {
        int endIndex = Mathf.Min(scrollInitDatas.Count, scrollItems.Count);
        middleIndex = Mathf.FloorToInt(endIndex / 2);
    }

    public bool canScrollToBefore
    {
        get
        {
            return middleIndex < scrollInitDatas.Count - 1;
        }
    }

    public bool canScrollToAfter
    {
        get
        {
            return middleIndex > 0;
        }
    }

    public bool haveData(GameObject item)
    {
        if (null == item) return true;
        int itemIndex = scrollItems.IndexOf(item);
        if (itemIndex < 0) return true;
        int dif = itemIndex - Mathf.FloorToInt(scrollItems.Count / 2);
        return GetData(middleIndex + dif) != null;
    }
    
    public void Scroll(bool bSameAsBeforeItem)
    {
        if (bSameAsBeforeItem && !canScrollToBefore) return;
        if (!bSameAsBeforeItem && !canScrollToAfter) return;

        middleIndex += bSameAsBeforeItem ? 1 : -1;
    }

    public void UpdateData()
    {
        int minItem = Mathf.FloorToInt(scrollItems.Count / 2);
   
        for(int i = minItem, m=0;i>=0;--i,++m)
        {
            scrollItems[i].GetComponent<ScrollToNeighborItemData>().UpdateData(GetData(middleIndex - m));
        }

        for(int i=minItem+1, m=1; i<scrollItems.Count;++i,++m)
        {
            scrollItems[i].GetComponent<ScrollToNeighborItemData>().UpdateData(GetData(middleIndex + m));
        }
    }

    Object GetData(int index)
    {
        if (scrollInitDatas.Count <= 0) return null;
        if (index < 0 || index >= scrollInitDatas.Count) return null;
        return scrollInitDatas[index];
    }
}

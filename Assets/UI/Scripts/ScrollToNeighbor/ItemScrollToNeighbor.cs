using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScrollToNeighbor : MonoBehaviour {

	public void ScrollItem(ScrollToNeighborParam scrollParam)
    {
        List<GameObject> scrollItems = scrollParam.initItems;
        if (null == scrollItems)
            return;
        float duration = scrollParam.duration;
        Sequence sequece = scrollParam.sequece;

        int index = scrollItems.IndexOf(gameObject);
        if (index < 0) return;
        index = scrollParam.bSameAsBeforeItem ? index - 1 : index + 1;
        if (index < 0 || index >= scrollItems.Count) return;
        if (!scrollParam.havaData(scrollItems[index])) return;

        Tweener tmove = transform.DOLocalMove(scrollParam.initScrollItemsLocalPosition[index], duration);
        Tweener tscale = transform.DOScale(scrollParam.initScrollItemsLocalScale[index], duration);
        transform.SetSiblingIndex(scrollParam.initScrollItemsSiblingIndex[index]);

        if (null != sequece)
        {
            sequece.Join(tmove).Join(tscale);
        }
    }

    public void ScrollItemEnd(ScrollToNeighborParam scrollParam)
    {
        List<GameObject> scrollItems = scrollParam.initItems;
        if (null == scrollItems)
            return;
        float duration = scrollParam.duration;
        Sequence sequece = scrollParam.sequece;

        int index = scrollItems.IndexOf(gameObject);
        if (index < 0) return;
        transform.localPosition = scrollParam.initScrollItemsLocalPosition[index];
        transform.localScale = scrollParam.initScrollItemsLocalScale[index];
        transform.SetSiblingIndex(scrollParam.initScrollItemsSiblingIndex[index]);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScrollToNeighbor : MonoBehaviour {

	public void ScrollItem(ScrollToNeighborParam scrollParam)
    {
        List<GameObject> scrollItems = scrollParam.curScrollItems;
        if (null == scrollItems)
            return;
        bool bSameAsBeforeItem = scrollParam.bSameAsBeforeItem;
        float duration = scrollParam.duration;
        Sequence sequece = scrollParam.sequece;

        int index = scrollItems.IndexOf(gameObject);
        if (index < 0) return;


        Tweener tmove = transform.DOLocalMove(scrollParam.curScrollPositions[index], duration);
        Tweener tscale = transform.DOScale(scrollParam.curScrollLocalScales[index], duration);

        if (null != sequece)
        {
            sequece.Join(tmove).Join(tscale);
        }
    }
}

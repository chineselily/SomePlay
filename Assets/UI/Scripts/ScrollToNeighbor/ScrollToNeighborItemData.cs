using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollToNeighborItemData : BaseItemData {

    Image map;
    Image map_mask;
    Button btn;
    override protected void InitItem()
    {
        base.InitItem();
        btn = GetComponent<Button>();
        if (null != btn)
            btn.onClick.AddListener(onBtnClick);
        map = transform.Find("map").GetComponent<Image>();
        map_mask = transform.Find("map_mask").GetComponent<Image>();
    }

    public override void UpdateData(Object data)
    {
        base.UpdateData(data);

        if(bHaveData)
            map.sprite = (Sprite)data;
    }

    private void onBtnClick()
    {
        Debug.Log("ClickMe");
    }
}

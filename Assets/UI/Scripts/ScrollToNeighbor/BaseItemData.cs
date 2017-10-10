using UnityEngine;
using System.Collections;

public class BaseItemData : MonoBehaviour {

    protected object mData = null;
    protected Object mDataObj = null;
    protected bool bHaveData = false;

    void Awake()
    {
        InitItem();
    }

    virtual protected void InitItem()
    {

    }

    virtual public void UpdateData(object data)
    {
        bHaveData = (data != null && !data.Equals(default(object)));
        mData = data;

        UpdateVisible();
    }

    virtual public void UpdateData(Object data)
    {
        bHaveData = (data != null && !data.Equals(default(Object)));
        mDataObj = data;

        UpdateVisible();
    }

    virtual protected void UpdateVisible()
    {
        transform.gameObject.SetActive(bHaveData);
    }
    /// <summary>
    /// 更新属性，有些属性
    /// </summary>
    virtual public void UpdateAttribute(params object[] param)
    {

    }
}

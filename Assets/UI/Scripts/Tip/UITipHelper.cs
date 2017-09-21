using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using System;

public class UITipHelper
{
    private static Dictionary<string, UITipPageData> mallTips;
    public static TTUIPage ShowTip<T>(int maxNum=-1) where T : TTUIPage, new()
    {
        Type t = typeof(T);
        string pageName = t.ToString();
        return GetPageData(pageName, maxNum).ShowTip<T>();
    }
    public static void CloseTip<T>()where T:TTUIPage, new()
    {
        Type t = typeof(T);
        string pageName = t.ToString();
        GetPageData(pageName).CloseTip();
    }
    private static UITipPageData GetPageData(string pageName, int maxNum=-1)
    {
        if (null == mallTips)
            mallTips = new Dictionary<string, UITipPageData>();
        UITipPageData tpage = null;
        if (mallTips.ContainsKey(pageName))
        {
            tpage = mallTips[pageName];
            tpage.maxNum = maxNum;
            return tpage;
        }
        tpage = new UITipPageData(pageName, maxNum);
        mallTips[pageName] = tpage;
        return tpage;
    }

}
class UITipPageData
{
    public string tipPageName { get; private set; }
    public int maxNum { get;  set; }
    public int curNum { get; private set; }
    public List<UITipData> tips { get; private set; }
    public UITipPageData(string ttipPageName, int tmaxNum)
    {
        tipPageName = ttipPageName;
        maxNum = tmaxNum;
        curNum = 0;
        tips = new List<UITipData>();
    }

    public TTUIPage ShowTip<T>() where T : TTUIPage, new()
    {
        TTUIPage tpage = null;
        if (maxNum > 0 && curNum >= maxNum)
            tpage = tips[0].uiPage;
        else
            tpage = CreateNew<T>();

        curNum += 1;
        return tpage;
    }
    public void CloseTip()
    {
        if (null == tips || tips.Count <= 0) return;
        UITipData tdata = tips[0];
        tdata.ClosePage();
        tips.RemoveAt(0);
    }
    private TTUIPage CreateNew<T>() where T : TTUIPage, new()
    {
        Type t = typeof(T);
        string pageName = t.ToString();
        TTUIPage page = GetTTUIPage(pageName);
        int createMode;
        if (null == page)
        {
            TTUIPage.ShowPage<T>();
            page = GetTTUIPage(pageName);
            createMode = 1;
        }
        else
        {
            TTUIPage tpage = new T();
            tpage.gameObject =
            Lean.LeanPool.Spawn(page.gameObject, page.transform.localPosition, page.transform.localRotation, page.transform.parent);
            tpage.transform = page.gameObject.transform;
            tpage.Awake(page.gameObject);
            tpage.Active();
            tpage.Refresh();
            createMode = 2;
            page = tpage;
        }
        UITipData tdata = new UITipData(page, curNum, createMode);
        tips.Add(tdata);
        tips.Sort((UITipData ua, UITipData ub)=>
        {
            return ua.index - ub.index;
        });
        return page;
    }

    private TTUIPage GetTTUIPage(string pageName)
    {
        if (null == TTUIPage.allPages || string.IsNullOrEmpty(pageName) || !TTUIPage.allPages.ContainsKey(pageName))
            return null;
        return TTUIPage.allPages[pageName];
    }
}

class UITipData
{
    public TTUIPage uiPage { get; private set; }
    public int index { get; private set; }
    /// <summary>
    /// 创建方式；1--TTUIpage, 2 -- lean pool
    /// </summary>
    public int createMode { get; set; }
    public UITipData(TTUIPage page, int tindex, int tcreatMode)
    {
        uiPage = page;
        index = tindex;
        createMode = tcreatMode;
    }

    public void ClosePage()
    {
        uiPage.type.ToString();
        string ttype = uiPage.GetType().ToString();
        if (1 == createMode)
            TTUIPage.ClosePage(ttype);
        else if (2 == createMode)
        {
            uiPage.Hide();
            Lean.LeanPool.Despawn(uiPage.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour {

    [HideInInspector]
    public Transform target = null;

    private Transform preTarget = null;

    [HideInInspector]
    public Transform mask;//设为public变量，用于其他脚本调用进行删除

    [HideInInspector]
    public Transform maskPop = null;//提示的UIPrefab;

    private Transform preMaskPop = null;
    private bool ChangeStep = false;
    private Transform parent = null;//取得target在原来层级的父物体
    private int index = -1;//取得target在原来的父物体的序列层级
    

    public static GuideManager Instance { set; get; }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (ChangeStep) {

            ToNext();
            ChangeStep = false;
        }
    }

    private void LateUpdate() {
        if (target != preTarget) {
            
            //Recover
            if (parent != null && index != -1) {
                preTarget.parent = parent;
                preTarget.SetSiblingIndex(index);
            }

            preTarget = target;
            parent = target.parent;
            index = target.GetSiblingIndex();//获得目标物体的层级序列

            ChangeStep = true;
        }

        
    }

    private void InitialMask() {
        var go = new GameObject();
        Canvas UICanvas = GameObject.FindObjectOfType<Canvas>();
        go.transform.SetParent(UICanvas.transform);
        go.transform.localScale = Vector3.one;
        go.name = "mask";
        go.AddComponent<RectTransform>();
        go.AddComponent<Image>();

        go.GetComponent<Image>().raycastTarget = true;
        go.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        go.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        go.GetComponent<RectTransform>().anchorMax = Vector2.one;
        go.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        go.transform.SetAsLastSibling();

        mask = go.transform;
    }

    private void ToNext() {

        //check mask
        if (mask == null) {
            InitialMask();
        } else {
            mask.gameObject.SetActive(true);
        }

        //设置显示物置前
        target.parent = mask.parent;//确保当前显示的UI的父物体为canvas，避免与其他UI的父子层级耦合，造成不小心连带删除的问题
        target.SetAsLastSibling();

        //设置maskPop
        if (maskPop != preMaskPop) {
            
            if(preMaskPop != null) {
                preMaskPop.gameObject.SetActive(false);//此处可以加入对象管理池进行释放
            }
            
            if(maskPop != null) { 
                maskPop = Instantiate(maskPop, mask.position, mask.rotation);
                maskPop.parent = mask.parent;//此处可以放入弹出框层级
                maskPop.SetAsLastSibling();
                maskPop.gameObject.SetActive(true);//确保prefab物体实例化后是激活状态，如果prefab本身是未激活状态，则需开启此剧
            }

            preMaskPop = maskPop;
        }
    }
}

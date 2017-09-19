using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideBtnTest : MonoBehaviour {

    public Transform target;

    void Start() {
        Button btn = this.GetComponent<Button>();
        if (btn != null) {
            btn.onClick.AddListener(() => {
                StartCoroutine("Simulate");
            });
        }


    }
    

    IEnumerator Simulate() {
        //点击按钮后如果有mask遮罩，则关闭
        Transform mask = GuideManager.Instance.mask;
        if (mask != null) {
            mask.gameObject.SetActive(false);
        }
        //点击按钮后如果有提示，则关闭
        Transform maskPop = GuideManager.Instance.maskPop;
        if (maskPop != null) {
            maskPop.gameObject.SetActive(false);
        }
        //执行主逻辑
        Debug.Log("I");
        yield return new WaitForSeconds(1);
        Debug.Log("love");
        yield return new WaitForSeconds(3);
        Debug.Log("U");
        yield return new WaitForSeconds(1);
        //下一步
        GameObject obj = Resources.Load("UIPrefab/Notice") as GameObject;
        GuideManager.Instance.maskPop = obj.transform;
        GuideManager.Instance.target = target;
    }
}

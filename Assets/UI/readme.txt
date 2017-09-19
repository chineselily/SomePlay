
//"Resources/UIPrefabs"下制作UI预制物体

//"Scripts/UI"下对应相对应的prefab进行编程，参考UIVScroll

//格式
using UnityEngine;
using System.Collections;
using TinyTeam.UI;
using UnityEngine.UI;

public class UIVScroll : TTUIPage {

    public UIVScroll() : base(UIType.Fixed, UIMode.DoNothing, UICollider.None) {
        uiPath = "UIPrefabs/V_Scroll";
    }

    public override void Awake(GameObject go) {

        for(int i = 0;i < 6; i++) {
			//添加按钮
            var btn = new GameObject();
            btn.transform.SetParent(this.gameObject.transform.Find("Layout"));
            btn.transform.localScale = Vector3.one;
            btn.AddComponent<Image>();
            btn.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 300);
            btn.AddComponent<Button>();
			//绑定点击事件
            //btn.GetComponent<Button>().onClick.AddListener(() => {
            //    TTUIPage.ClosePage<UIVScroll>();
            //});
        }
    }


}

using UnityEngine;
using System.Collections;
using TinyTeam.UI;
using UnityEngine.UI;

public class UIVScroll : TTUIPage {

    public UIVScroll() : base(UIType.Fixed, UIMode.DoNothing, UICollider.None) {
        uiPath = "UIPrefabs/V_Scroll";
    }

    public override void Awake(GameObject go) {

        Debug.Log(this.gameObject.name);

        for(int i = 0;i < 6; i++) {
            var btn = new GameObject();
            btn.transform.SetParent(this.gameObject.transform.Find("Layout"));
            btn.transform.localScale = Vector3.one;
            btn.AddComponent<Image>();
            btn.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 300);
            btn.AddComponent<Button>();
            //btn.GetComponent<Button>().onClick.AddListener(() => {
            //    TTUIPage.ClosePage<UIVScroll>();
            //});
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language3DText : MonoBehaviour {

    public string key;

    private void OnEnable() {
        SetStr();
    }

    public void SetStr() {
        this.GetComponent<TextMesh>().text = LocalizationText.GetText(key);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageUIText : MonoBehaviour {

    public string key;

    private void OnEnable() {
        SetStr();
    }

    public void SetStr() {
        this.GetComponent<UnityEngine.UI.Text>().text = LocalizationText.GetText(key);
    }
}

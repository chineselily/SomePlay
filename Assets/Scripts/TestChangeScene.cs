using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestChangeScene : MonoBehaviour
{
    Button btn;
    private void Start()
    {
        btn = GetComponent<Button>();

        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        Debug.Log(scene.buildIndex);

        btn.onClick.AddListener(ClickBtn);
    }

    public void ClickBtn()
    {
        SceneManager.LoadScene("Drone_City");

        //asy = SceneManager.LoadSceneAsync(“demo2“);
    }

    //void Update()
    //{
    //    StartCoroutine(TiaoZhuanNext());

    //}

    //IEnumerator TiaoZhuanNext()
    //{

    //    asy.allowSceneActivation = false;

    //    if (sl.value != 1 || asy.progress < 0.9f)
    //    {
    //        sl.value += 0.01f;
    //        yield return new WaitForEndOfFrame();
    //    }
    //    else if (asy.progress >= 0.9f)
    //    {
    //        asy.allowSceneActivation = true;
    //    }
    //}
}

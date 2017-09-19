using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BornManager : MonoBehaviour {

    public GameObject[] prefabs;
    public Transform bornCenter;//出生位置的半径中心点
    private float r;//半径
    private Vector3 bornPos;//出生点坐标
    public float intervalT = 3;
    private float t = 0;
    private GameObject LastBornedObj;
    private float globalScale = 1;

    private void Awake() {
        globalScale = Camera.main.transform.position.y;//根据摄像机FPV高度确定场景物体缩放
    }

    private void Update() {

        if(prefabs.Length == 0) {
            return;
        }

        t += Time.deltaTime;
        if (t < 3) {
            return;
        }

        t = 0;
        r = bornCenter.GetComponent<MeshFilter>().mesh.bounds.size.x * bornCenter.localScale.x / 2;
        bornPos = new Vector3(
            bornCenter.position.x + r * Random.Range(-1.0f , 1.0f),
            bornCenter.position.y,
            bornCenter.position.z + r * Random.Range(-1.0f , 1.0f));

        Vector3 projCamPos = new Vector3(Camera.main.transform.position.x,bornCenter.position.y, Camera.main.transform.position.z);//Camera到bronCenter物体的映射点
        
        float dist = Vector3.Distance(bornPos, projCamPos);
        
        if(dist < 3) {
            return;
        }

        LastBornedObj = (GameObject)Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], bornPos, Quaternion.identity);
        LastBornedObj.transform.localScale = Vector3.zero;
        LastBornedObj.transform.DOScale(Vector3.one * globalScale, 1).SetEase(Ease.OutElastic);//动画
        LastBornedObj.layer = 10;//AR层

    }
}

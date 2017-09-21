using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TinyTeam.UI;

public class BornManagerBasedCamera : MonoBehaviour {

    //预制物体
    public GameObject[] prefabs;
    //以次物体为出生半径的中心点
    public Transform bornCenter;
    //生成的时间间隔
    [Tooltip("生成物体的时间间隔")]
    public float intervalT = 3;
    //计时器T
    private float t = 0;
    private float globalScale = 1;
    //生成的物体之间的最小间距
    [Range(1, 15)]
    public float bornSpacing;
    //场景中显示的物体的集合
    public static List<GameObject> activelist = new List<GameObject>();

    private void Awake() {
        globalScale = Camera.main.transform.position.y;//根据摄像机FPV高度确定场景物体缩放
        //TTUIPage.ShowPage<UIVScroll>();
    }

    private void Update() {

        if(prefabs.Length == 0) {
            return;
        }
        //计算时间  小于3秒不执行
        t += Time.deltaTime;
        if (t < intervalT) {
            return;
        }

        //计算生成的半径，以borncenter物体为中心点，并以次物体体边长x的一半为半径
        float r = bornCenter.GetComponent<MeshFilter>().mesh.bounds.size.x * bornCenter.localScale.x / 2;
        //半径内的随机点
        Vector3 bornPos = new Vector3(
            bornCenter.position.x + r * Random.Range(-1.0f , 1.0f),
            bornCenter.position.y,
            bornCenter.position.z + r * Random.Range(-1.0f , 1.0f));
        //计算和场景中激活物体的间距
        for(int i = 0; i < activelist.Count; i++) {
            float space = Vector3.Distance(bornPos,activelist[i].transform.position);
            if(space < bornSpacing) {
                return;
            }
        }

        //Camera到bronCenter物体的垂点
        Vector3 projCamPos = new Vector3(Camera.main.transform.position.x,bornCenter.position.y, Camera.main.transform.position.z);
        //计算垂点到随机点的距离
        float dist = Vector3.Distance(bornPos, projCamPos);
        
        if(dist < 3) {
            return;
        }
        //如果半径大于3，通过对象池生成物体
        var clone = Lean.LeanPool.Spawn(prefabs[Random.Range(0, prefabs.Length)], bornPos, Quaternion.identity, null);//随机数包前不包后
        //加入list用于后续判断生成物体之间的间距
        activelist.Add(clone.gameObject);
        
        //判断生成物的属性
        if(clone.tag == "coin") {
            clone.transform.rotation = Quaternion.Euler(90, 0, 0);
            //如果没有旋转脚本,添加
            if (!clone.GetComponent<DeltaRotate>()) {
                clone.AddComponent<DeltaRotate>();
                //设置是否绕着世界坐标旋转
                clone.GetComponent<DeltaRotate>().WorldY = true;
                //设置旋转速度
                clone.GetComponent<DeltaRotate>().rotateSpeed = 3;
            }
        }else if(clone.tag == "tree") {
            //通过dotween设置生成物体时的动画
            //如果没有脚本,添加
            if (!clone.GetComponent<MeshCollider>()) {
                clone.AddComponent<MeshCollider>();
            }
            if (!clone.GetComponent<TreeDespawn>()) {
                clone.AddComponent<TreeDespawn>();
            }
            clone.transform.localScale = Vector3.zero;
            clone.transform.DOScale(Random.Range(0.4f, 1.0f) * globalScale, 0.5f).SetEase(Ease.OutElastic);
            //设置AR层，使之被reflection probe影响
            clone.layer = 10;
        }

        //添加消失机制

        t = 0;
    }
}

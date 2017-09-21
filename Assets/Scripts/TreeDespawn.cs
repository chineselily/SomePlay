using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeDespawn : MonoBehaviour {
    [SerializeField]
    private float maxdist = Mathf.Sqrt(125);//5平方 + 10平方 开根号

    void Update () {
        Vector3 TreeVector = transform.position - Camera.main.transform.position;
        //点击计算方向
        float f = Vector3.Dot(Camera.main.transform.forward, TreeVector);
        //计算与摄像头的距离
        float dist = Vector3.Distance(Camera.main.transform.position,transform.position);

        //如果在摄像机背面，并且超过距离,则despawn
        if(f < 0 && dist > maxdist) {
            if (BornManagerBasedCamera.activelist.Contains(this.gameObject)) {
                Lean.LeanPool.Despawn(this.gameObject);
                BornManagerBasedCamera.activelist.Remove(this.gameObject);
            }
        }
        //如果在摄像机前方，并且超过距离
        else if(f > 0 && dist > maxdist) {
            if (BornManagerBasedCamera.activelist.Contains(this.gameObject)) {
                //缩放动画
                transform.DOScale(0, 1);
                //动画时间过后，despawn
                Lean.LeanPool.Despawn(this.gameObject,1);
                BornManagerBasedCamera.activelist.Remove(this.gameObject);
            }
        }
	}
}

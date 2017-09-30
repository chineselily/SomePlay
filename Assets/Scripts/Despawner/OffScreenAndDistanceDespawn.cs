using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenAndDistanceDespawn : MonoBehaviour {

    public float offScreenMaxdist = 10f;
    public float onScreenMaxdist = float.MaxValue;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!gameObject.activeSelf) return;

        Vector3 TreeVector = transform.position - Camera.main.transform.position;
        //点击计算方向
        float f = Vector3.Dot(Camera.main.transform.forward, TreeVector);
        //计算与摄像头的距离
        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);

        //如果在摄像机背面，并且超过距离,则despawn
        if (f < 0 && dist > offScreenMaxdist)
        {
            Lean.LeanPool.Despawn(this.gameObject);
        }
        //如果在摄像机前方，并且超过距离
        else if (f > 0 && dist > onScreenMaxdist)
        {
            //缩放动画
            transform.DOScale(0, 1);
            //动画时间过后，despawn
            Lean.LeanPool.Despawn(this.gameObject, 1);
        }
    }
}

//Vector3 pos = Camera.main.WorldToViewportPoint(go.transform.position);
//bool isVisible = (Camera.main.orthographic || pos.z > 0f)
//&& ((pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f));
//ret = isVisible==true ?1:0;

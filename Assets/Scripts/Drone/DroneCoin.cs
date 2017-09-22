using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCoin : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        RandomCreater mycreater = transform.GetComponent<RandomCreater>();
        if (null != mycreater)
            mycreater.callBack = UpdateCoinComponent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateCoinComponent(List<RandomCellData> list)
    {
        if (null == list) return;
        RandomCellData tcell;
        for(int i=0,count=list.Count;i<count;++i)
        {
            tcell = list[i];
            if(null != tcell.cellObject)
            {
                if (tcell.cellObject.GetComponent<Spin>() == null)
                {
                    tcell.cellObject.AddComponent<Spin>();
                    tcell.cellObject.GetComponent<Spin>().rotationsPerSecond = new Vector3(0, 0, 0.1f);
                }
                if (tcell.cellObject.GetComponent<ColliderDebuger>() == null)
                    tcell.cellObject.AddComponent<ColliderDebuger>();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneObjectBornLogic : MonoBehaviour {

    private void Start()
    {
        TinyTeam.UI.TTUIPage.ShowPage<DroneMainUI>();
    }
    public void RandomCreateObject(GameObject go)
    {
        if (null == go) return;

        if (go.GetComponent<ColliderDespawn>() == null)
        {
            if (go.GetComponent<Rigidbody>() == null)
            {
                Rigidbody tr = go.AddComponent<Rigidbody>();
                tr.isKinematic = false;
                tr.useGravity = false;
                tr.mass = int.MaxValue;
                tr.drag = int.MaxValue;
                tr.angularDrag = int.MaxValue;
            }
            go.AddComponent<ColliderDespawn>();
        }

        if (go.GetComponent<OffScreenAndDistanceDespawn>() == null)
        {
            go.AddComponent<OffScreenAndDistanceDespawn>();
        }

        if (go.transform.tag == DroneMapLogic.coin)
        {
            go.transform.localRotation = Quaternion.Euler(90, 0, -180);
            go.transform.Translate(0, 0, 0.01f);
            if (go.GetComponent<Spin>() == null)
            {
                Spin ts = go.AddComponent<Spin>();
                ts.rotationsPerSecond = new Vector3(0, 0, 0.1f);
            }
            go.GetComponent<OffScreenAndDistanceDespawn>().offScreenMaxdist = 10f;
        }
        else if(go.transform.tag == DroneMapLogic.treasure)
        {
            if(go.GetComponent<TimeUpDespawn>() == null)
            {
                TimeUpDespawn tc = go.AddComponent<TimeUpDespawn>();
                tc.lifeTime = DroneMapLogic.instance.disappearTime;
            }
        }
    }
}

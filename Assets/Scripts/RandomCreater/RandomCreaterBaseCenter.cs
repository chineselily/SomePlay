using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreaterBaseCenter : MonoBehaviour {

    public GameObject prefabToCreate;
    //以此物体为出生半径的中心点
    public Transform bornCenter;
    //绑定的父物体
    public Transform bornParent;
    //生成的时间间隔,秒为单位
    [Tooltip("生成物体的时间间隔,为0则一次性生成完")]
    public float intervalT = 3;
    //生成的物体之间的最小间距
    public float bornSpacing = 1f;
    //最大数量, -1，则无限量
    public int maxNum = -1;
    //相对于父物体的限定范围，可以不设置
    public Vector3 parentMinPosition = new Vector3(0, 0, 0);
    public Vector3 parentMaxPosition = new Vector3(0, 0, 0);

    //对象池
    List<GameObject> randomObjects;
    //计时器T
    private float t = 0;
    void Start () {
        if (null == bornParent) bornParent = transform;
        randomObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if (t < intervalT || null == prefabToCreate) return;
        //数量判断
        RemoveInvalidObjects();
        if (isReachMaxNum) return;

        int createNum = intervalT <= 0 ? maxNum-randomObjects.Count : 1;
        Vector3 localPos;
        for(int i=0;i<createNum;i+=1)
        {
            bool bret = GetBornPosition(out localPos);
            if (bret)
            {
                GameObject tobj = Lean.LeanPool.Spawn(prefabToCreate, localPos, Quaternion.Euler(0, 0, 0), bornParent);
                tobj.SendMessageUpwards("RandomCreateObject", tobj);
                Debug.Log("create, myname=" + tobj.name + " active=" + tobj.activeSelf + " localpo=" + tobj.transform.localPosition);
                randomObjects.Add(tobj);
            }
        }
        t = 0;
    }

    bool GetBornPosition(out Vector3 bornposition)
    {
        bornposition = Vector3.zero;
        if (null != bornCenter)
        {
            //计算生成的半径，以borncenter物体为中心点，并以次物体体边长x的一半为半径
            Bounds centerB = bornCenter.GetComponent<MeshFilter>().mesh.bounds;
            //尝试20次找到适合的点
            int count = 0;
            while (count < 20)
            {
                float rx = centerB.size.x * bornCenter.lossyScale.x / 2;
                float ry = centerB.size.y * bornCenter.lossyScale.y / 2;
                float rz = centerB.size.z * bornCenter.lossyScale.z / 2;
                //半径内的随机点
                Vector3 worldPos = new Vector3(bornCenter.transform.position.x + rx * Random.Range(-1f, 1f),
                                               bornCenter.transform.position.y + ry * Random.Range(-1f, 1f),
                                               bornCenter.transform.position.z + rz * Random.Range(-1f, 1f));
                bornposition = bornParent.InverseTransformPoint(worldPos);
                if (inMinMaxPositionRange(bornposition) && isBornSpaceOk(bornposition))
                {
                    return true;
                }
            }
        }
        else if (parentMinPosition != parentMaxPosition)
        {
            int count = 0;
            while (count < 20)
            {
                bornposition.x = Random.Range(parentMinPosition.x, parentMaxPosition.x);
                bornposition.y = Random.Range(parentMinPosition.y, parentMaxPosition.y);
                bornposition.z = Random.Range(parentMinPosition.z, parentMaxPosition.z);
                if(isBornSpaceOk(bornposition))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool inMinMaxPositionRange(Vector3 tpos)
    {
        if (parentMinPosition.x != parentMaxPosition.x && !(tpos.x >= parentMinPosition.x && tpos.x <= parentMaxPosition.x)) return false;
        if (parentMinPosition.y != parentMaxPosition.y && !(tpos.y >= parentMinPosition.y && tpos.y <= parentMaxPosition.y)) return false;
        if (parentMinPosition.z != parentMaxPosition.z && !(tpos.z >= parentMinPosition.z && tpos.z <= parentMaxPosition.z)) return false;
        return true;
    }

    bool isReachMaxNum
    {
        get
        {
            if (maxNum < 0) return false;
            return randomObjects.Count >= maxNum;
        }
    }
    void RemoveInvalidObjects()
    {
        for (int i = randomObjects.Count - 1; i >= 0; i -= 1)
        {
            if (null == randomObjects[i] || !randomObjects[i].activeSelf)
            {
                randomObjects.RemoveAt(i);
            }
        }
    }
    bool isBornSpaceOk(Vector3 tops)
    {
        if (bornSpacing <= 0f) return true;
        Vector3 tobj;
        for(int i=0;i<randomObjects.Count;i+=1)
        {
            tobj = randomObjects[i].transform.localPosition;
            if (Vector3.Distance(tops, tobj) < bornSpacing) return false;
        }
        return true;
    }
}

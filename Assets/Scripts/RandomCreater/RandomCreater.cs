using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreater : MonoBehaviour {

    public GameObject prefabToCreate;
    public Vector3 prefabRotation = new Vector3(0, 0, 0);
    public Vector3 minPosition = new Vector3(0, 0, 0);
    public Vector3 maxPosition = new Vector3(1, 1, 1);
    public int maxNum = 10;
    public float minDistance = 2f;

    [HideInInspector]
    public delegate void CreateCallBack(List<RandomCellData> objects);
    [HideInInspector]
    public CreateCallBack callBack;
    [HideInInspector]
    public List<RandomCellData> randomObjects { get; private set; }

    public void Start()
    {
        Create();
    }

    [ContextMenu("Create")]
    public void Create()
    {
        if (null == prefabToCreate) return;
        RemoveInvalidCells();
        randomObjects = RandomStrategy.Create(this);
        if (null != callBack)
            callBack.Invoke(randomObjects);
    }
    
    public int needCreateNum
    {
        get
        {
            if (null == randomObjects) return maxNum;
            return Mathf.Max(0, maxNum - randomObjects.Count);
        }
    }

    private void RemoveInvalidCells()
    {
        if (null == randomObjects) return;
        for(int i=0,count=randomObjects.Count;i<count;i+=1)
        {
            if(null == randomObjects[i].cellObject || !randomObjects[i].cellObject.activeSelf)
            {
                randomObjects.RemoveAt(i);
                --i;
            }
        }
    }
}
public class RandomCellData
{

    public Vector3 cellPosition;
    public string cellName;
    public GameObject cellObject;

    public void Create(GameObject prefab, Transform tparent, Vector3 localposition, string name, Vector3 localRotation)
    {
        cellName = name;
        cellPosition = localposition;
        cellObject = Lean.LeanPool.Spawn(prefab,localposition,Quaternion.Euler(localRotation.x,localRotation.y,localRotation.z),tparent);
        cellObject.name = name;
    }

    public void Destroy()
    {
        if (null == cellObject) return;
        Lean.LeanPool.Despawn(cellObject);
        cellObject = null;
    }
}

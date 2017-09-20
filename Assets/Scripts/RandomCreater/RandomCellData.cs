using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCellData{

    public Vector3 cellPosition;
    public string cellName;
    public GameObject cellObject;

    public void Create(GameObject prefab, Transform parent, Vector3 localposition, string name)
    {
        cellName = name;
        cellPosition = localposition;
        cellObject = Lean.LeanPool.Spawn(prefab);
        cellObject.transform.SetParent(parent);
        cellObject.transform.localPosition = localposition;
        cellObject.name = name;
    }

    public void Destroy()
    {
        if (null == cellObject) return;
        Lean.LeanPool.Despawn(cellObject);
        cellObject = null;
    }
}

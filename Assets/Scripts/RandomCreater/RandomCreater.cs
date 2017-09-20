using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreater : MonoBehaviour {

    public GameObject prefabToCreate;
    public Vector3 minPosition = new Vector3(0, 0, 0);
    public Vector3 maxPosition = new Vector3(1, 1, 1);
    public int maxNum = 10;
    public float minDistance = 2f;
    
    public List<RandomCellData> randomObjects { get; set; }

    public void Start()
    {
        Create();
    }

    [ContextMenu("Create")]
    public void Create()
    {
        if (null == prefabToCreate) return;
        DestroyRandomObjects();
        RandomStrategy.Create(this);
    }

    private void DestroyRandomObjects()
    {
        if (null == randomObjects) return;
        for(int i=0,count=randomObjects.Count;i<count;i+=1)
        {
            randomObjects[i].Destroy();
        }
        randomObjects.Clear();
    }
}

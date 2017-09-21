using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStrategy
{
    public static void Create(RandomCreater randomCreater)
    {
        if (null == randomCreater) return;
        List<RandomCellData> listData = new List<RandomCellData>();
        RandomCellData tData;
        Vector3 tv = Vector3.zero;
        for(int i=0;i<randomCreater.maxNum;)
        {
            tv.x = UnityEngine.Random.Range(randomCreater.minPosition.x, randomCreater.maxPosition.x);
            tv.y = UnityEngine.Random.Range(randomCreater.minPosition.y, randomCreater.maxPosition.y);
            tv.z = UnityEngine.Random.Range(randomCreater.minPosition.z, randomCreater.maxPosition.z);
            if(isValidCell(listData,tv,randomCreater.minDistance))
            {
                i += 1;
                tData = new RandomCellData();
                tData.Create(randomCreater.prefabToCreate, randomCreater.transform, tv, randomCreater.prefabToCreate.name + i);
                listData.Add(tData);
            }
        }
        randomCreater.randomObjects = listData;
    }

    public static bool isValidCell(List<RandomCellData> listData, Vector3 checkPosition, float minDistance)
    {
        if (null == listData || listData.Count <= 0) return true;
        Vector3 tposition;
        for(int i=0,count=listData.Count;i<count;i+=1)
        {
            tposition = listData[i].cellPosition;
            if (Mathf.Abs(tposition.x - checkPosition.x) + Mathf.Abs(tposition.y - checkPosition.y) + Mathf.Abs(tposition.z - checkPosition.z) < minDistance) return false;
        }
        return true;
    }
}

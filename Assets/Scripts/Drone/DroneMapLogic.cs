using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType { Easy, Normal, Hard}
public class DroneMapLogic{
    public static string coin = "coin";
    public static string treasure = "treasure";

    private static DroneMapLogic mInstance;
    public static DroneMapLogic instance
    {
        get
        {
            if (null == mInstance)
                mInstance = new DroneMapLogic();
            return mInstance;
        }
    }

    private MapType mmapType = MapType.Easy;
    //吃到宝物后的buffer时间
    private int bufferTime;
    public MapType mapType
    {
        get { return mmapType; }
        set
        {
            mmapType = value;
            bufferTime = mmapType == MapType.Easy ? 10 : (mmapType == MapType.Normal ? 5 : 3);
        }
    }

    public int disappearTime
    {
        get
        {
            return mmapType == MapType.Easy ? 10 : (mmapType == MapType.Normal ? 5 : 3);
        }
    }

    private int startBufferTimeStamp;
    public void AddBuffer(int timeStamp)
    {
        startBufferTimeStamp = timeStamp;
    }
    //是否有宝物加成效果
    public bool isBufferAdd { get { return System.DateTime.Now.Second - startBufferTimeStamp <= bufferTime; } }

    public int addScore
    {
        get
        {
            if (!isBufferAdd) return 1;
            return mmapType == MapType.Easy ? 2 : (mmapType == MapType.Normal ? 5 : 10);
        }
    }

    public int reduceScore
    {
        get
        {
            return mmapType == MapType.Easy ? 5 : (mmapType == MapType.Normal ? 10 : 20);
        }
    }
}

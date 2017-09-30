using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneColliderLogic : MonoBehaviour {

    private ColliderEffectRedVibrate myVibrate;
    // Use this for initialization
    private void Awake()
    {
        myVibrate = GetComponent<ColliderEffectRedVibrate>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == DroneMapLogic.coin)
        {
            int number = DroneMapLogic.instance.addScore;
            (UITipHelper.ShowTip<TipNumber>(MathUtil.WorldToTTUIPosition(other.transform.position)) as TipNumber).SetNumber(number);
        }
        else if(other.tag == DroneMapLogic.treasure)
        {
            DroneMapLogic.instance.AddBuffer(System.DateTime.Now.Second);
        }
        else
        {
            if (null != myVibrate)
                myVibrate.Vibrate();
            int number = DroneMapLogic.instance.reduceScore;
            (UITipHelper.ShowTip<TipNumber>() as TipNumber).SetNumber(-number);
        }
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

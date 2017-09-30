﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColliderLogic : MonoBehaviour {

    private ColliderEffectRedVibrate myVibrate;
    // Use this for initialization
    private void Awake()
    {
        myVibrate = GetComponent<ColliderEffectRedVibrate>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "coin")
        {
            if(other.GetComponent<ColliderDespawn>() == null)
                Lean.LeanPool.Despawn(other);
            (UITipHelper.ShowTip<TipNumber>(MathUtil.WorldToTTUIPosition(other.transform.position)) as TipNumber).SetNumber(1);
        }
        else
        {
            if (null != myVibrate)
                myVibrate.Vibrate();
            (UITipHelper.ShowTip<TipNumber>() as TipNumber).SetNumber(-1);
        }
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

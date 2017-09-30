using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUpDespawn : MonoBehaviour {

    public int lifeTime = 0;

    float bornTime;
	// Use this for initialization
	void Start () {
        bornTime = Time.realtimeSinceStartup;
	}
    private void OnEnable()
    {
        bornTime = Time.realtimeSinceStartup;
    }
    // Update is called once per frame
    void Update () {
        if (lifeTime <= 0) return;
        if (!gameObject.activeSelf) return;

        if (Time.realtimeSinceStartup - bornTime >= lifeTime)
            Lean.LeanPool.Despawn(gameObject);
	}
}

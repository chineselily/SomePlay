using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaRotate : MonoBehaviour {

    [SerializeField]
    public float rotateSpeed = 10;
    public bool WorldY = false;
    
	void Update () 
    {
        if (!WorldY) { 
            Transform child = transform.GetChild(0);
            child.RotateAround(child.right,Time.deltaTime * rotateSpeed);
            transform.RotateAround(transform.forward, Time.deltaTime * rotateSpeed);
        } else {
            transform.RotateAround(Vector3.up, Time.deltaTime * rotateSpeed);
        }
    }
}

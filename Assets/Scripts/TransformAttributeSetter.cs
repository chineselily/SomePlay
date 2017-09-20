using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAttributeSetter : MonoBehaviour {

    public Vector3 InitRotation;
	// Use this for initialization
	void Start () {
        transform.localRotation = Quaternion.Euler(InitRotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDebuger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter, myname="+transform.name+" collision.name="+collision.transform.name);
    }

    public void OnCollisionStay(Collision collision)
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter, myname=" + transform.name + " Collider.name=" + other.transform.name);
    }

    public void OnTriggerStay(Collider other)
    {
        
    }
}

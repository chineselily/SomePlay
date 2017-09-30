using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDespawn : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        //Debug.Log("ColliderDebuger-Awake,myname=" + transform.name);
    }
    void Start () {
        //Debug.Log("ColliderDebuger-start,myname=" + transform.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        Despawn();
        //Debug.Log("OnCollisionEnter, myname="+transform.name+" collision.name="+collision.transform.name);
    }

    public void OnCollisionStay(Collision collision)
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Despawn();
        //Debug.Log("OnTriggerEnter, myname=" + transform.name + " Collider.name=" + other.transform.name);
    }

    public void OnTriggerStay(Collider other)
    {

    }

    private void Despawn()
    {
        if (!gameObject.activeSelf) return;
        Lean.LeanPool.Despawn(gameObject);
        Debug.Log("Despawn, myname=" + transform.name + " active=" + gameObject.activeSelf + " localpo="+transform.localPosition);
    }
}

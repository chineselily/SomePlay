using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRecieverFollow : MonoBehaviour {

    public Transform target;

    private void Update() {
        transform.position = new Vector3(target.position.x, 0, target.position.z);
    }
}

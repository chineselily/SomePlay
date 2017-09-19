using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBLEnvRotAdjust : MonoBehaviour {
    
	
	void Update () {
#if UNITY_IPHONE || UNITY_ANDROID
        Vector3 angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(Input.acceleration.x * -90, angles.y,angles.z );//-1~1
#endif
    }
}

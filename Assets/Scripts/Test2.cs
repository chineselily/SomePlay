using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour {

	
	void OnGUI () {

        GUILayout.Label(Input.acceleration.x.ToString());
        GUILayout.Label(Input.acceleration.y.ToString());
        GUILayout.Label(Input.acceleration.z.ToString());
    }
}

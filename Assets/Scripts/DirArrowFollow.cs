using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirArrowFollow : MonoBehaviour {

    public Transform FollowTarget;
    public bool enableY;
    private Vector3 initialOffset;
    public float CameraHeight = 1;//现实中设备在地面上时，摄像头的离地高度(米)
    public Transform testLookatTarget;

    private void Awake() {
        if(FollowTarget != null) {
            initialOffset = transform.position - Camera.main.transform.position;
        }
        
    }

    void Update () {

        if (FollowTarget == null) {
            return;
        }
        
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        //避免穿插地面的mask
        if (transform.position.y < 0) {
            transform.position = new Vector3(transform.position.x,0, transform.position.z);
        }

        if (!enableY) {
            transform.position = new Vector3(transform.position.x, CameraHeight - 0.1f, transform.position.z);
        }

        //Test Code
        testLookatTarget.position = new Vector3(testLookatTarget.position.x, transform.position.y, testLookatTarget.position.z);
        transform.LookAt(testLookatTarget);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardByKeyboard : MonoBehaviour {

    public float speed = 10.0F;
    void Update() {
        float translation = Input.GetAxis("Vertical") * speed;
        translation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
    }
}

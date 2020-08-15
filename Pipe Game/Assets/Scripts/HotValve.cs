using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotValve : MonoBehaviour {

    public float turnSpeed;
    float turnLimit;
    bool turning;

    void Start() {

    }

    void FixedUpdate() {
        print(transform.eulerAngles.z);
        if (turning) {
            if (turnLimit == 355)
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            else if (turnLimit == 5)
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            if (transform.eulerAngles.z >= 355 || transform.eulerAngles.z <= 5) {
                turning = false;
            }
        }
    }

    private void OnMouseDown() {
        turning = true;
        if (transform.eulerAngles.z >= 355)
            turnLimit = 5;
        else if (transform.eulerAngles.z <= 5)
            turnLimit = 355;
    }
}

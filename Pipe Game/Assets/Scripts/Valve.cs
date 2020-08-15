using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour {

    public float turnSpeed;
    float turnLimit;
    bool turning;  

    public enum ValveType {Hot, Cold};
    public ValveType valveType;

    void Start() {

    }

    void FixedUpdate() {
        if (turning) {
            if (turnLimit == 350)
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            else if (turnLimit == 10)
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            if (transform.eulerAngles.z >= 350) {
                turning = false;
                if (valveType == ValveType.Hot)
                    GameManager.Instance.hotOn = true;
                else if (valveType == ValveType.Cold)
                    GameManager.Instance.coldOn = true;
            } else if (transform.eulerAngles.z <= 10) {
                turning = false;
                if (valveType == ValveType.Hot)
                    GameManager.Instance.hotOn = false;
                else if (valveType == ValveType.Cold)
                    GameManager.Instance.coldOn = false;
            }
        }
    }

    private void OnMouseDown() {
        turning = true;
        if (transform.eulerAngles.z >= 350)
            turnLimit = 10;
        else if (transform.eulerAngles.z <= 10)
            turnLimit = 350;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour {

    public float turnSpeed;
    float timeWhenTurned;
    bool isOn;
    bool turning;

    public enum ValveType { Hot, Cold, Boiler };
    public ValveType valveType;

    void Start() {
        isOn = true;
    }

    void FixedUpdate() {
        if (turning) {
            if (Time.time >= timeWhenTurned) {
                turning = false;
                if (!isOn) {
                    isOn = true;
                    if (valveType == ValveType.Hot)
                        GameManager.Instance.hotOn = true;
                    else if (valveType == ValveType.Cold)
                        GameManager.Instance.coldOn = true;
                    else if (valveType == ValveType.Boiler)
                        Stove.Instance.boilerFillingUp = false;
                }
                else {
                    isOn = false;
                    if (valveType == ValveType.Hot)
                        GameManager.Instance.hotOn = false;
                    else if (valveType == ValveType.Cold)
                        GameManager.Instance.coldOn = false;
                    else if (valveType == ValveType.Boiler)
                        Stove.Instance.boilerFillingUp = true;
                }
            }
        }
    }

    private void OnMouseDown() {
        if (!turning)
            timeWhenTurned = Time.time + turnSpeed;
        turning = true;                

        if (isOn) {
            //Play closing anim once
        }
        else {
            //Play opening anim once
        }
    }
}

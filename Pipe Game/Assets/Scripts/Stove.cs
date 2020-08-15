using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour {

    public float stoveTemp;
    public float targetTemp;
    public float stoveCoolingSpeed;
    public float stoveWarmingSpeed;
    public bool stoveWarmingUp;

    public float boilerAmt;
    public bool boilerFillOn;

    public bool fuelHoveringOnStove;

    void Start() {
        stoveTemp = 50f;
    }

    void FixedUpdate() {
        if (targetTemp > stoveTemp && stoveWarmingUp) {
            stoveTemp += stoveWarmingSpeed;
        }
        else if (targetTemp < stoveTemp || !stoveWarmingUp) {
            stoveWarmingUp = false;
            stoveTemp -= stoveCoolingSpeed;
            if (stoveTemp < 0)
                stoveTemp = 0;           
        }
        if (!stoveWarmingUp) {
            targetTemp = stoveTemp;
        }
    }

    public void AddFuel(float amt) {
        if (targetTemp == 0)
            targetTemp = stoveTemp;

        amt = ((100 - targetTemp) / 100) * amt;
        targetTemp += amt;        
        if (targetTemp > 100)
            targetTemp = 100;
        stoveWarmingUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Fuel")) {
            fuelHoveringOnStove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Fuel")) {
            fuelHoveringOnStove = false;
        }
    }
}

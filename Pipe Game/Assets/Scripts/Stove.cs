using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour {
    public static Stove Instance;

    public float boilerWaterTemp;
    public float targetBoilerWaterTemp;
    public float stoveCoolingSpeed;
    public float stoveWarmingSpeed;
    public bool stoveWarmingUp;

    public float boilerWaterAmt;
    public float boilerWaterUsageSpeed;
    public bool boilerFillingUp;
    public float boilerWaterFillingSpeed;
    public bool boilerHasHotWater;

    public bool fuelHoveringOnStove;

    void Start() {
        Instance = this;
        boilerWaterTemp = 50f;
        boilerWaterAmt = 50f;
        boilerHasHotWater = true;
    }

    void FixedUpdate() {
        //water temperature changing
        if (targetBoilerWaterTemp > boilerWaterTemp && stoveWarmingUp) {
            boilerWaterTemp += stoveWarmingSpeed;
        }
        else if (targetBoilerWaterTemp < boilerWaterTemp || !stoveWarmingUp) {
            stoveWarmingUp = false;
            boilerWaterTemp -= stoveCoolingSpeed;
            if (boilerWaterTemp < 0) {
                boilerWaterTemp = 0;
                boilerHasHotWater = false;
            }

        }
        if (!stoveWarmingUp) {
            targetBoilerWaterTemp = boilerWaterTemp;
        }

        //water amount changing
        if (boilerFillingUp) {
            boilerWaterAmt += boilerWaterFillingSpeed;
            if (boilerWaterAmt > 100) {
                boilerWaterAmt = 100;
            }
        }
        if (GameManager.Instance.hotOn) {
            boilerWaterAmt -= boilerWaterUsageSpeed;
            if (boilerWaterAmt < 0) {
                boilerWaterAmt = 0;
                boilerHasHotWater = false;
            }
            else if (boilerWaterAmt >= 0 && boilerFillingUp)
                boilerHasHotWater = true; //boiler has water again if ran out completely
        }
    }

    public void AddFuel(float amt) {
        //getting rid of initial "0" -value for target temp
        if (targetBoilerWaterTemp == 0)
            targetBoilerWaterTemp = boilerWaterTemp;

        amt = ((100 - targetBoilerWaterTemp) / 100) * amt;
        targetBoilerWaterTemp += amt;
        if (targetBoilerWaterTemp > 100)
            targetBoilerWaterTemp = 100;
        stoveWarmingUp = true;
        boilerHasHotWater = true;  //water is hot again if heat ran out from stove completely
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

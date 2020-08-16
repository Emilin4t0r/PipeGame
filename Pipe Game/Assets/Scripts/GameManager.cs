using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public bool grabbingSomething;

    public float waterAmtInBath;
    public float bathWaterUsageSpeed;
    public float bathWaterFillSpeed;
    public bool hasWaterFlow;

    public float waterTemp;
    public float tempDemand;

    public bool tempDemandFulfilled;

    public float happiness;
    public float happinessLossSpeed;

    public bool hotOn;
    public bool coldOn;
    public float tempChangeSpeed;

    //Just triggers for card drop animation, doesn't matter if they are left on even after correct water temp adjustment
    public bool requestingHot;
    public bool requestingCold;

    public float requestTime;
    public float requestInterval;

    GameObject stove;

    void Awake() {
        Instance = this;
        hotOn = true;
        coldOn = true;
        waterTemp = 50f;
        tempDemand = 50f;
        waterAmtInBath = 100f;
        happiness = 50;

        stove = GameObject.Find("Stove");
    }

    void FixedUpdate() {
        //Request card functionality        
        if (Time.time >= requestTime) {
            SendRequest();
            requestTime = Time.time + requestInterval;
        }

        //Water temperature adjusting
        if ((hotOn && !coldOn && Stove.Instance.boilerHasHotWater) || (Stove.Instance.boilerFillingUp && hotOn && coldOn)) {
            waterTemp += tempChangeSpeed;
            hasWaterFlow = true;
        }
        if ((!hotOn && coldOn && !Stove.Instance.boilerFillingUp) || (!Stove.Instance.boilerHasHotWater && coldOn && hotOn)) {
            waterTemp -= tempChangeSpeed;
            hasWaterFlow = true;
        }        
        if (hotOn && coldOn && !Stove.Instance.boilerFillingUp && Stove.Instance.boilerHasHotWater)
            hasWaterFlow = true;
        if ((!hotOn && !coldOn) || (coldOn && !hotOn && Stove.Instance.boilerFillingUp) || (!coldOn && hotOn && !Stove.Instance.boilerHasHotWater) || (hotOn && coldOn && Stove.Instance.boilerFillingUp && !Stove.Instance.boilerHasHotWater))
            hasWaterFlow = false;


        //Avoiding overflow
        if (waterTemp > 100)
            waterTemp = 100;
        else if (waterTemp < 0)
            waterTemp = 0;

        //Water amount in bath
        if (!hasWaterFlow) {
            waterAmtInBath -= bathWaterUsageSpeed;
            if (waterAmtInBath <= 0)
                waterAmtInBath = 0;
        } else if (hasWaterFlow) {
            waterAmtInBath += bathWaterFillSpeed;
            if (waterAmtInBath >= 100)
                waterAmtInBath = 100;
        }

        //Happiness calculations
        if (waterAmtInBath == 0) {
            happiness -= happinessLossSpeed;
        }
        if (requestingHot && waterTemp >= tempDemand) {
            tempDemandFulfilled = true;
        } else if (requestingCold && waterTemp <= tempDemand) {
            tempDemandFulfilled = true;
        }
        if (tempDemandFulfilled) {
            happiness += 10;
            tempDemandFulfilled = false;
            requestingCold = false;
            requestingHot = false;
        }
        if (happiness > 100)
            happiness = 100;
        if (happiness < 0)
            happiness = 0;
    }

    void SendRequest() {
        requestingHot = false;
        requestingCold = false;        
        tempDemand = Random.Range(waterTemp + 50, waterTemp - 50);

        //Avoiding under- or overtemperature (if random number passes 100 or 0)
        if (waterTemp <= 10 && tempDemand <= 10)
            tempDemand += 50;
        else if (waterTemp >= 90 && tempDemand >= 90)
            tempDemand -= 50;

        //Avoiding overflow
        if (tempDemand > 100)
            tempDemand = 100;
        else if (tempDemand < 0)
            tempDemand = 0;

        //Determining if hot or cold card
        if (tempDemand > waterTemp) {
            requestingHot = true;
        }
        else if (tempDemand < waterTemp) {
            requestingCold = true;
        }


    }
}

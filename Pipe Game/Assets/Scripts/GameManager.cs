using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public bool grabbingSomething;

    public float waterTemp;
    public float tempDemand;
    public float happiness;

    public bool hotOn;
    public bool coldOn;
    public float tempChangeSpeed;

    //Just triggers for card drop animation, doesn't matter if they are left on even after correct water temp adjustment
    public bool requestingHot;
    public bool requestingCold;

    public float requestTime;
    public float requestInterval;

    void Start() {
        Instance = this;
        hotOn = true;
        coldOn = true;
        waterTemp = 50f;
        tempDemand = 50f;
    }

    void FixedUpdate() {
        //Request card functionality        
        if (Time.time >= requestTime) {
            SendRequest();
            requestTime = Time.time + requestInterval;
        }

        //Water temperature adjusting
        if (hotOn && !coldOn)
            waterTemp += tempChangeSpeed;
        else if (!hotOn && coldOn)
            waterTemp -= tempChangeSpeed;

        if (waterTemp > 100)
            waterTemp = 100;
        else if (waterTemp < 0)
            waterTemp = 0;        
    }

    void SendRequest() {
        requestingCold = false;
        requestingHot = false;
        tempDemand = Random.Range(waterTemp + 50, waterTemp - 50);

        //Avoiding under- or overtemperature (if random number passes 100 or 0)
        if ((waterTemp == 0 && tempDemand < 0) || waterTemp == 100 && tempDemand > 100)
            tempDemand *= -1;

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

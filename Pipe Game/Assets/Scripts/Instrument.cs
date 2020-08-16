using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{

    public enum InstrumentType { WaterTempGauge, BoilerTempMeter, BoilerWaterMeter, HappinessMeter, WaterPressureGauge }
    public InstrumentType instrumentType;

    public GameObject indicator;
    public GameObject secondaryIndicator;

    void Start()
    {
        if (instrumentType == InstrumentType.WaterTempGauge) {
            indicator = transform.GetChild(0).gameObject;
        }
        if (instrumentType == InstrumentType.BoilerTempMeter) {
            indicator = transform.GetChild(0).gameObject;
            secondaryIndicator = transform.GetChild(1).gameObject;
        }
        if (instrumentType == InstrumentType.BoilerWaterMeter) {
            indicator = transform.GetChild(0).gameObject;
        }
    }

    void FixedUpdate()
    {
        if (indicator != null) {
            if (instrumentType == InstrumentType.WaterTempGauge) {
                indicator.transform.eulerAngles = new Vector3(0, 0, (GameManager.Instance.waterTemp - 50) * -2);
            }
            if (instrumentType == InstrumentType.BoilerTempMeter) {
                indicator.transform.localScale = new Vector3(1, Stove.Instance.boilerWaterTemp / 100, 1);
                secondaryIndicator.transform.localScale = new Vector3(1, Stove.Instance.targetBoilerWaterTemp / 100, 1);
            }
            if (instrumentType == InstrumentType.BoilerWaterMeter) {
                indicator.transform.localScale = new Vector3(1, Stove.Instance.boilerWaterAmt / 100, 1);
            }
        }
    }
}

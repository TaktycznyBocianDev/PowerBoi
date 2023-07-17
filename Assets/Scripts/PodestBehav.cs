using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodestBehav : MonoBehaviour
{
    public bool isCharger;
    public GameObject batteryPositionPodest;
    public GameObject stationFiller;
    public GameObject currentBattery;
    public Material materialIfOn, materialIfOff;


    public void TakeBattery(GameObject battery)
    {
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOn;
        BatteryBehav currentBattery =  battery.GetComponent<BatteryBehav>();

        if (!isCharger) 
        {
            currentBattery.SetBattery(2, transform, batteryPositionPodest.transform.position);
        }
        else
        {
            currentBattery.SetBattery(3, transform, batteryPositionPodest.transform.position);
        }
      
    }

    public void BatteryIsGone()
    {
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOff;
    }
}

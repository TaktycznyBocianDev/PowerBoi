using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodestBehav : MonoBehaviour
{
    public BatteryMode batteryMode;
    public float dropRate;
    public float minTimeToEnergyDrop;
    public float maxTimeToEnergyDrop;
    public float cafeMakerDropRate;
    [Space(15)]
    public bool isCharger;
    public GameObject batteryPositionPodest;
    public GameObject stationFiller;
    public GameObject currentBattery;
    public Material materialIfOn, materialIfOff;
    public StationConfig config;
    

    private void Start()
    {
       config = new StationConfig(dropRate, minTimeToEnergyDrop, maxTimeToEnergyDrop, cafeMakerDropRate);
    }

    public void TakeBattery(GameObject battery)
    {
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOn;
        BatteryBehav currentBattery =  battery.GetComponent<BatteryBehav>();

        if (!isCharger) 
        {
            currentBattery.SetBattery(2, transform, batteryPositionPodest.transform.position, batteryPositionPodest.transform.rotation);
        }
        else
        {
            currentBattery.SetBattery(3, transform, batteryPositionPodest.transform.position, batteryPositionPodest.transform.rotation);
        }
      
    }

    public void BatteryIsGone()
    {
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOff;
    }
}

public class StationConfig
{
    public float dropRate;
    public float minTimeToEnergyDrop;
    public float maxTimeToEnergyDrop;
    public float cafeMakerDropRate;

    public StationConfig(float dropRate, float minTimeToEnergyDrop, float maxTimeToEnergyDrop, float cafeMakerDropRate)
    {
        this.dropRate = dropRate;
        this.minTimeToEnergyDrop = minTimeToEnergyDrop;
        this.maxTimeToEnergyDrop = maxTimeToEnergyDrop;
        this.cafeMakerDropRate = cafeMakerDropRate;
    }
}

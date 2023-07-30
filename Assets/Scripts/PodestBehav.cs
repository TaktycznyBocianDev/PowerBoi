using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class PodestBehav : MonoBehaviour
{
    public string StationName;
    [Space(15)]
    public BatteryMode batteryMode;
    public float timeRate;
    public float minTimeToEnergyDrop;
    public float maxTimeToEnergyDrop;
    public float cafeMakerDropRate;
    [Space(15)]
    public GameObject batteryPositionPodest;
    public StationConfig config;


    public virtual void OnEnable()
    {
        config = new StationConfig(timeRate);
        config.batteryLevelDrop += BatteryIsGone;
    }

    public virtual void OnDisable()
    {
        config.batteryLevelDrop -= BatteryIsGone;
    }

    public virtual void TakeBattery(GameObject battery) { }

    public virtual void BatteryIsGone() { }

}

public class StationConfig
{
    public PowerOnBatteryLevel stationDisplay;

    public float dropRate;
    public float minTimeToEnergyDrop;
    public float maxTimeToEnergyDrop;
    public float cafeMakerDropRate;

    public ShootingConfig shootingConfig;

    public delegate void BateryLevelDropToZero();
    public event BateryLevelDropToZero batteryLevelDrop;

    public StationConfig(float dropRate, float minTimeToEnergyDrop, float maxTimeToEnergyDrop, float cafeMakerDropRate, ShootingConfig shootingConfig)
    {
        this.dropRate = dropRate;
        this.minTimeToEnergyDrop = minTimeToEnergyDrop;
        this.maxTimeToEnergyDrop = maxTimeToEnergyDrop;
        this.cafeMakerDropRate = cafeMakerDropRate;
        this.shootingConfig = shootingConfig;

    }

    public StationConfig(float dropRate, float minTimeToEnergyDrop, float maxTimeToEnergyDrop, float cafeMakerDropRate, PowerOnBatteryLevel stationDisplay)
    {
        this.dropRate = dropRate;
        this.minTimeToEnergyDrop = minTimeToEnergyDrop;
        this.maxTimeToEnergyDrop = maxTimeToEnergyDrop;
        this.cafeMakerDropRate = cafeMakerDropRate;
        this.stationDisplay = stationDisplay;
    }

    public StationConfig(float dropRate)
    {
        this.dropRate = dropRate;
    }

    public void BateryLevelEvent()
    {
        if (batteryLevelDrop != null) batteryLevelDrop();
    }
}

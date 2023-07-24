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
    [Space(15)]
    public PowerOnBatteryLevel stationDisplay;
    public Image displayImage;
    public TMP_Text textMeshPro;
    private Color startingDisplayColor;

    private void OnEnable()
    {
        startingDisplayColor = displayImage.color;
        textMeshPro.text = StationName;
        SetDispay(Color.red);
        config = new StationConfig(dropRate, minTimeToEnergyDrop, maxTimeToEnergyDrop, cafeMakerDropRate, stationDisplay);
        config.batteryLevelDrop += BatteryIsGone;
    }

    private void OnDisable()
    {
        config.batteryLevelDrop -= BatteryIsGone;
    }

    public void TakeBattery(GameObject battery)
    {
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOn;
        BatteryBehav currentBattery = battery.GetComponent<BatteryBehav>();
        SetDispay(startingDisplayColor);

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

        //Set display to red and start the alarm
        SetDispay(Color.red);

    }

    private void SetDispay(Color color)
    {
        displayImage.color = color;
        stationDisplay.SetEnergyOnDisplay(0f);
    }
}

public class StationConfig
{
    public PowerOnBatteryLevel stationDisplay;

    public float dropRate;
    public float minTimeToEnergyDrop;
    public float maxTimeToEnergyDrop;
    public float cafeMakerDropRate;

    public delegate void BateryLevelDropToZero();
    public event BateryLevelDropToZero batteryLevelDrop;

    public StationConfig(float dropRate, float minTimeToEnergyDrop, float maxTimeToEnergyDrop, float cafeMakerDropRate, PowerOnBatteryLevel stationDisplay)
    {
        this.dropRate = dropRate;
        this.minTimeToEnergyDrop = minTimeToEnergyDrop;
        this.maxTimeToEnergyDrop = maxTimeToEnergyDrop;
        this.cafeMakerDropRate = cafeMakerDropRate;
        this.stationDisplay = stationDisplay;
    }

    public void BateryLevelEvent()
    {
        if (batteryLevelDrop != null) batteryLevelDrop();
    }
}

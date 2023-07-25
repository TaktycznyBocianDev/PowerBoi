using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipStationBehav : PodestBehav
{
    [Space(10)]
    [Header("Set colors wher station is on/off")]
    public GameObject stationFiller;
    public Material materialIfOn, materialIfOff;
    [Space(10)]
    [Header("Display Settings")]
    public PowerOnBatteryLevel stationDisplay;
    public Image displayImage;
    public TMP_Text textMeshPro;
    private Color startingDisplayColor;

    override public void OnEnable()
    {
        startingDisplayColor = displayImage.color;
        textMeshPro.text = StationName;
        SetDispay(Color.red);
        config = new StationConfig(timeRate, minTimeToEnergyDrop, maxTimeToEnergyDrop, cafeMakerDropRate, stationDisplay);
        config.batteryLevelDrop += BatteryIsGone;
    }

    public override void TakeBattery(GameObject battery)
    {
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOn;
        SetDispay(startingDisplayColor);
        BatteryBehav currentBattery = battery.GetComponent<BatteryBehav>();
        currentBattery.SetBattery(2, transform, batteryPositionPodest.transform.position, batteryPositionPodest.transform.rotation);
    }

    public override void BatteryIsGone()
    {
        base.BatteryIsGone();
        SetDispay(Color.red);
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOff;
    }

    void SetDispay(Color color)
    {
        displayImage.color = color;
        stationDisplay.SetEnergyOnDisplay(0f);
    }
}

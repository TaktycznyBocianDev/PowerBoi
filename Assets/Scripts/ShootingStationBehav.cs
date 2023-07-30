using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShootingStationBehav : PodestBehav
{
    [Space(10)]
    [Header("Set colors wher station is on/off")]
    public GameObject stationFiller;
    public Material materialIfOn, materialIfOff;
    [Space(10)]
    [Header("Display Settings")]
    private ShootingConfig shootingConfig;
    public TMP_Text stationName, stationState;
    [Space(10)]
    [Header("Station States")]
    public string withOutBattery = "Weapons system ofline! Provide power immediately";
    public string withBattery = "Weapons system online and ready";

   

    override public void OnEnable()
    {
        stationName.text = StationName;
        batteryMode = BatteryMode.shootingStation;
        shootingConfig = new ShootingConfig(stationName, stationState, withOutBattery, withBattery);

        SetDispay(false);
        config = new StationConfig(timeRate, minTimeToEnergyDrop, maxTimeToEnergyDrop, cafeMakerDropRate, shootingConfig);
        config.batteryLevelDrop += BatteryIsGone;
    }

    public override void TakeBattery(GameObject battery)
    {
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOn;
        SetDispay(true);
        BatteryBehav currentBattery = battery.GetComponent<BatteryBehav>();
        currentBattery.SetBattery(2, transform, batteryPositionPodest.transform.position, batteryPositionPodest.transform.rotation);
    }

    public override void BatteryIsGone()
    {
        base.BatteryIsGone();
        SetDispay(false);
        stationFiller.GetComponentInChildren<Renderer>().sharedMaterial = materialIfOff;
    }

    void SetDispay(bool batteryIsIn)
    {
        if (batteryIsIn)
        {

            stationState.color = Color.green;
            stationState.text = withBattery;

        }
        else
        {
            stationState.color = Color.red;
            stationState.text = withOutBattery;
        }
    }
}

public class ShootingConfig
{
    public TMP_Text stationName, stationState;
    public string withOutBattery = "Weapons system ofline! Provide power immediately";
    public string withBattery = "Weapons system online and ready";

    public ShootingConfig(TMP_Text stationName, TMP_Text stationState, string withOutBattery, string withBattery)
    {
        this.stationName = stationName;
        this.stationState = stationState;
        this.withOutBattery = withOutBattery;
        this.withBattery = withBattery;
    }

    public void SetDispay(bool batteryIsIn)
    {
        if (batteryIsIn)
        {

            stationState.color = Color.green;
            stationState.text = withBattery;

        }
        else
        {
            stationState.color = Color.red;
            stationState.text = withOutBattery;
        }
    }
}
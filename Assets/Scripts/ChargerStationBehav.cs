using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerStationBehav : PodestBehav
{

    public override void TakeBattery(GameObject battery)
    {
        BatteryBehav currentBattery = battery.GetComponent<BatteryBehav>();
        currentBattery.SetBattery(3, transform, batteryPositionPodest.transform.position, batteryPositionPodest.transform.rotation);
    }

}
 
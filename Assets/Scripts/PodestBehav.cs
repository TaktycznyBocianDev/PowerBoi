using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodestBehav : MonoBehaviour
{
    public GameObject batteryPositionPodest;

    public void TakeBattery(GameObject battery)
    {
        BatteryBehav currentBattery =  battery.GetComponent<BatteryBehav>();

        currentBattery.SetBattery(2, transform, batteryPositionPodest.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class WithBatteryInteraction : MonoBehaviour
{
    public GameObject batteryPositionPlayer;

    bool holdBattery;
    GameObject batteryInHand;

    private void Start()
    {
        holdBattery = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (!holdBattery)
                {                   

                    BatteryBehav battery = hit.transform.GetComponent<BatteryBehav>();
                    if (battery != null)
                    {
                        batteryInHand = hit.transform.gameObject;
                        battery.SetBattery(1, transform, batteryPositionPlayer.transform.position);
                        holdBattery = true;
                    }
                }
                else
                {

                    PodestBehav podest = hit.transform.GetComponent<PodestBehav>();

                    if (podest != null)
                    {
                        podest.TakeBattery(batteryInHand);
                        holdBattery = false;
                    }
                    else
                    {
                        if (hit.transform.gameObject.CompareTag("Ground"))
                        {
                            batteryInHand.GetComponent<BatteryBehav>().SetBattery(0, null, hit.point);
                            holdBattery = false;

                        }
                    }
                }
            }
            
            
        }

    }
}

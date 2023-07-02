using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class WithBatteryInteraction : MonoBehaviour
{
    public GameObject batteryPositionPlayer;
    public GameObject batteryPositionPodest;


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
            if (!holdBattery)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.forward, out hit);

                BatteryBehav battery = hit.transform.GetComponent<BatteryBehav>();
                if (battery != null)
                {
                    batteryInHand = hit.transform.gameObject;
                    batteryInHand.GetComponent<Rigidbody>().useGravity = false;
                    battery.transform.SetParent(transform, false);                
                    battery.transform.position = batteryPositionPlayer.transform.position;
                    holdBattery = true;
                }
            }
            else
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.forward, out hit); 

                PodestBehav podest = hit.transform.GetComponent<PodestBehav>();
                if (podest != null)
                {
                    batteryInHand.GetComponent<Rigidbody>().useGravity = true;
                    batteryInHand.transform.SetParent(podest.transform, false);
                    batteryInHand.transform.position = batteryPositionPodest.transform.position;
                    holdBattery = false;
                }
                else
                {
                    batteryInHand.transform.SetParent(null, false);
                    batteryInHand.GetComponent<Rigidbody>().useGravity = true;
                    batteryInHand.GetComponent<Rigidbody>().AddForce(batteryInHand.transform.forward * 0.1f, ForceMode.Impulse);
                }
            }
            
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class BatteryBehav : MonoBehaviour
{
    public bool defaultEnergyLevel;
    public float maxEnergyLevel;
    public float currentEnergyLevel;
    public float dropRate;
    public PowerOnBatteryLevel powerOnBatteryLevel;

    Rigidbody rb;

    enum BatteryState
    {
        onFloor,
        inHand,
        inStation,
        inChargerStation
    }
    BatteryState currentState;
    BatteryState previousState;

    

    private void Start()
    {
        if (defaultEnergyLevel) currentEnergyLevel = maxEnergyLevel;
        powerOnBatteryLevel.SetMaxEnergy(maxEnergyLevel);
        currentState = BatteryState.onFloor;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (previousState != currentState)
        {
            OnChangedState();
        }
        powerOnBatteryLevel.SetEnergyOnDisplay(currentEnergyLevel);
        previousState = currentState;
    }

    public void SetBattery(int state, Transform newParent, Vector3 position, Quaternion rotation)
    {
        currentState = (BatteryState)state;
        SetBatteryParent(newParent);
        SetBatteryPosition(position);
        SetBatteryRotation(rotation);
    }


    void OnChangedState()
    {
        if (currentState == BatteryState.onFloor)
        {
            BatteryOnFloor();
        }
        if (currentState == BatteryState.inHand)
        {
            BatteryInHand();
        }
        if (currentState == BatteryState.inStation)
        {
            BatteryInStation();
        }
        if (currentState == BatteryState.inChargerStation)
        {
            BatteryInChargerStation();
        }
    }

    void BatteryOnFloor()
    {
        StopAllCoroutines();
        SetBatteryParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

    }

    void BatteryInHand()
    {
        StopAllCoroutines();
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    void BatteryInStation()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.localScale = new Vector3(0.8f, 0.4f, 0.8f);
        StartCoroutine(DropEnergy(GetComponentInParent<PodestBehav>().batteryMode, GetComponentInParent<PodestBehav>().config));
    }

    void BatteryInChargerStation()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.localScale = new Vector3(0.8f, 0.4f, 0.8f);
        StartCoroutine(AddEnergy());
    }

    public void SetBatteryParent(Transform newParent)
    {
        transform.SetParent(newParent);
    }

    public void SetBatteryPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetBatteryRotation(Quaternion rotation)
    {
        transform.rotation =  rotation;
    }

    public void SetBatteryToPlayer(GameObject Player)
    {
        transform.LookAt(Player.transform.position);
        transform.Rotate(0, 90, 0);
    }

    public IEnumerator DropEnergy(BatteryMode mode, StationConfig config)
    {
 
        while (currentEnergyLevel > 0)
        {
            switch (mode)
            {
                case BatteryMode.basic:
                    currentEnergyLevel--;
                    powerOnBatteryLevel.SetEnergyOnDisplay(currentEnergyLevel);
                    config.stationDisplay.SetMaxEnergy(maxEnergyLevel);
                    config.stationDisplay.SetEnergyOnDisplay(currentEnergyLevel);
                    yield return new WaitForSeconds(config.dropRate);
                    break;
                case BatteryMode.shootingStation:
                    yield return new WaitForSeconds(Random.Range(config.minTimeToEnergyDrop, config.maxTimeToEnergyDrop));
                    //currentEnergyLevel = 0;
                    config.stationDisplay.SetMaxEnergy(maxEnergyLevel);
                    powerOnBatteryLevel.SetEnergyOnDisplay(currentEnergyLevel);
                    config.stationDisplay.SetEnergyOnDisplay(currentEnergyLevel);
                    currentEnergyLevel = 0;
                    break;
                case BatteryMode.caffeMakerStation:
                    currentEnergyLevel -= 10;
                    config.stationDisplay.SetMaxEnergy(maxEnergyLevel);
                    powerOnBatteryLevel.SetEnergyOnDisplay(currentEnergyLevel);
                    config.stationDisplay.SetEnergyOnDisplay(currentEnergyLevel);
                    yield return new WaitForSeconds(config.cafeMakerDropRate);
                    break;

            }
            
        }
        if (currentEnergyLevel <= 0)
        {
            config.BateryLevelEvent();
            Debug.Log("No more energy in this battery!");
            currentEnergyLevel = 0;
        }
    }

    public IEnumerator AddEnergy()
    {
        while (currentEnergyLevel < maxEnergyLevel)
        {
            currentEnergyLevel++;
            powerOnBatteryLevel.SetEnergyOnDisplay(currentEnergyLevel);
            yield return new WaitForSeconds(dropRate);
        }

        if (currentEnergyLevel > 100)
        {
            Debug.Log("Max power!");
            currentEnergyLevel = maxEnergyLevel;
        }
    }
}

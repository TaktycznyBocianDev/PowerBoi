using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class BatteryBehav : MonoBehaviour
{
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
        currentEnergyLevel = maxEnergyLevel;
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

    public void SetBattery(int state, Transform newParent, Vector3 position)
    {
        currentState = (BatteryState)state;
        SetBatteryParent(newParent);
        SetBatteryPosition(position);
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
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

    }

    void BatteryInHand()
    {
        StopAllCoroutines();
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.rotation = Quaternion.Euler(0, -45, 0);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    void BatteryInStation()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = new Vector3(0.8f, 0.4f, 0.8f);
        StartCoroutine(DropEnergy());
    }

    void BatteryInChargerStation()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
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

    public IEnumerator DropEnergy()
    {
        while (currentEnergyLevel > 0)
        {
            currentEnergyLevel--;
            powerOnBatteryLevel.SetEnergyOnDisplay(currentEnergyLevel);
            yield return new WaitForSeconds(dropRate);
        }

        if (currentEnergyLevel < 0)
        {
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

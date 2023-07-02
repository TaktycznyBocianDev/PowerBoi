using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class BatteryBehav : MonoBehaviour
{
    Rigidbody rb;


    enum BatteryState
    {
        onFloor,
        inHand,
        inStation
    }
    BatteryState currentState;
    BatteryState previousState;

    private void Start()
    {
        currentState = BatteryState.onFloor;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (previousState != currentState)
        {
            OnChangedState();
        }

        previousState = currentState;
    }

    public void SetBattery(int state)
    {
        currentState = (BatteryState)state;
    }


    void OnChangedState()
    {
        if(currentState == BatteryState.onFloor)
        {
            BatteryOnFloor();
        }
        if (currentState == BatteryState.inHand)
        {
            BatteryInHand();
        }
        if (currentState == BatteryState.inStation)
        {
            BatteryInSation();
        }
    }

    void BatteryOnFloor()
    {
        SetBatteryParent(null);
        rb.useGravity = true;
    }

    void BatteryInHand()
    {
        rb.useGravity = false;
    }

    void BatteryInSation()
    {

    }

    public void SetBatteryParent(Transform newParent)
    {
        transform.SetParent(newParent);
    }

    public void SetBatteryPosition(Vector3 position)
    {
        transform.position = position;
    }
}

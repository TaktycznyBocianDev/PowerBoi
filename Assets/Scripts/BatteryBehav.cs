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

    public void SetBattery(int state, Transform newParent, Vector3 position)
    {
        currentState = (BatteryState)state;
        SetBatteryParent(newParent);
        SetBatteryPosition(position);
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
            BatteryInStation();
        }
    }

    void BatteryOnFloor()
    {
        SetBatteryParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        
    }

    void BatteryInHand()
    {
        
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

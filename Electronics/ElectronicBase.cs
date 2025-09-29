using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ElectronicBase
{

    public float pendingPower = 0.0f;

    public float Power = 0.0f;

    public Wiring WiringScript;
    public float forwardFactor = 1f;
    public GameObject Block;

    public GameObject Orb;

    public ElectronicBase[] connections = new ElectronicBase[1];

    public string propagate = "NO";
    public virtual bool Action()
    {
        DistributePower();
        Orb.transform.position = Block.transform.position + Block.transform.up + Block.transform.up * (Power + 1) * 0.1f;
        return true;
    }
    public virtual bool CheckCondition()
    {
        return true;
    }
    public virtual bool DistributePower()
    {
        if (connections[0] != null)
        {
            connections[0].AddPower(Power);
            //connections[0].Action();
        }
        SetTruePower(0f);

        Power = pendingPower;
        pendingPower = 0f;
        return true;
    }
    
    public virtual void SetPower(float newPower) {
        pendingPower = newPower;
    }
    
    public virtual void SetTruePower(float newPower) {
        Power = newPower;
    }

    public virtual void AddPower(float newPower)
    {
        pendingPower += newPower;
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resistor : ElectronicBase
{

    public float resistance;
    public override bool Action()
    {

        DistributePower();
        
        Power = pendingPower;
        
        return true;
    }
    public override bool DistributePower()
    {
        SetPower(0f);
        connections[0].AddPower(1);
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndGate : ElectronicBase
{

    public override bool Action()
    {
        if (CheckCondition())
        {
            DistributePower();
            Power = pendingPower;
        }
        return true;
    }

    public AndGate() {
        forwardFactor = 2f;
    }
    public override bool CheckCondition()
    {
        if (ports[0].Power >= 1f && ports[1].Power >= 1f)
        {
            return true;
        }
        return false;
    }

    public override bool DistributePower()
    {


        connections[0].AddPower(ports[0].Power + ports[1].Power);

        ports[0].SetPower(0f);
        ports[1].SetPower(0f);
        
        return true;
    }

}
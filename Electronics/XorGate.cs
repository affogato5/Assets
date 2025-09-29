
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XorGate : ElectronicBase
{


    public new float forwardFactor = 2f;

    public override bool Action()
    {
        if (CheckCondition())
        {
            DistributePower();
            Power = pendingPower;
        }
        return true;
    }

    public override bool CheckCondition()
    {
        if ((ports[0].Power == 1f || ports[1].Power == 1f) && !(ports[0].Power == 1f && ports[1].Power == 1f))
        {
            return true;
        }
        return false; 
    }

    public override bool DistributePower()
    {
        connections[0].SetPower(Power);
        SetPower(0f);
        return true;
    }
}
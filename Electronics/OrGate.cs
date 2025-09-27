
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrGate : ElectronicBase
{

    public ElectronicBase[] ports = new ElectronicBase[2];
    public override bool Action()
    {
        if (CheckCondition())
        {
            DistributePower();
            Power = pendingPower;
        }
        return true;
    }

    public OrGate() {
        forwardFactor = 2f;
    }
    public override bool CheckCondition()
    {
        if (ports[0].Power >= 1f || ports[1].Power >= 1f)
        {
            return true;
        }
        return false;
    }
}
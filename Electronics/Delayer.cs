
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delayer : ElectronicBase
{

    public float delay = 0f;
    public float goalDelay = 1f;
    public override bool Action()
    {
        DistributePower();
        Power = pendingPower;
        return true;
    }
    public override bool DistributePower()
    {

        if (delay == goalDelay)
        {
            SetPower(0f);
            connections[0].AddPower(1);
        }
        else
        {
            delay += 1f;
        }

        return true;
    }
}
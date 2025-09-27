
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NButton : ElectronicBase
{
    public override bool Action()
    {
        if (WiringScript.GKD(KeyCode.G))
        {
            SetTruePower(10f);
        }
        DistributePower();
        
        return true;
    }

    public override bool CheckCondition()
    {
        return true;
    }

    public override bool DistributePower()
    {
        connections[0].SetPower(Power);
        SetTruePower(0f);

        Power = pendingPower;
        pendingPower = 0f;
        
        return true;
    }
}

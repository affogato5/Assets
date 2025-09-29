
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NButton : ElectronicBase
{

    public KeyCode keycode = KeyCode.Q;

    public NButton(KeyCode givenKeycode)
    {
        keycode = givenKeycode;
    }

    public override bool Action()
    {
        if (WiringScript.GKD(keycode))
        {
            SetTruePower(10f);
        }
        DistributePower();
        Orb.transform.position = Block.transform.position + Block.transform.up + Block.transform.up * (Power + 1) * 0.1f;

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

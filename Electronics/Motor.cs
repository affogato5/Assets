
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : ElectronicBase
{
    public GameObject fullBody;

    public Motor()
    {

    }
    public override bool Action()
    {
        return true;
    }
    public override bool DistributePower()
    {
        SetPower(0f);
        connections[0].AddPower(1);
        return true;
    }
}
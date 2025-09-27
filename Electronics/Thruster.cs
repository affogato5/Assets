
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : ElectronicBase
{

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : ElectronicBase
{
     

    public Motor()
    {

    }
    public override bool Action()
    {
        if (Power > 1f)
        {
            GameObject fullBody = Block.GetComponent<GameObjectStorage>().storage;
            if (fullBody != null)
            {
                fullBody.transform.RotateAround(Block.transform.position, Vector3.up, 2 * Time.deltaTime);
            }
        }
        return true;
    }
    public override bool DistributePower()
    {
        SetPower(0f);
        connections[0].AddPower(1);
        return true;
    }
}
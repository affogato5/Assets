
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
        Power += pendingPower;
        pendingPower = 0f;

        if (Power > 0f)
        {
            GameObject fullBody = Block.GetComponent<GameObjectStorage>().storage;
            if (fullBody != null)
            {
                Power = Power - 1f;
                fullBody.transform.RotateAround(Block.transform.position, Block.transform.forward, 3);
            }
            
        }

        Orb.transform.position = Block.transform.position + Block.transform.up + Block.transform.up * (Power + 1) * 0.1f;


        return true;
    }
    public override bool DistributePower()
    {
    

        return true;
    }
}
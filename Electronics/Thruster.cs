
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;

public class Thruster : ElectronicBase
{

    public GameObject fullBody;

    public Thruster()
    {

    }
    public override bool Action()
    {
        Power += pendingPower;
        pendingPower = 0f;

        if (Power > 0f)
        {
            fullBody = Block.GetComponent<GameObjectStorage>().storage;

            if (fullBody == null)
            {
                if (Block.transform.parent != null)
                {
                    while (true)
                    {
                        fullBody = Block.transform.parent.gameObject;
                        if (fullBody.transform.parent != null)
                        {
                            fullBody = fullBody.transform.parent.gameObject;
                        }
                        else
                        {
                            break;
                        }

                    }

                }
            }

            if (fullBody != null)
                {

                    Power = Power - 1f;
                    fullBody.transform.position = fullBody.transform.position + Block.transform.forward * 0.05f;
                    fullBody.transform.Rotate(0, Block.transform.localPosition.x, 0);
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
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wiring : MonoBehaviour
{
    public float Power = 0.0f;

    public Dictionary<int, ElectronicBase> listOfParts = new Dictionary<int, ElectronicBase>();
    public GameObject originBlock;
    public ElectronicBase origin;

    public float timePassed = 0f;
    public Vector3 forward;


    public GameObject orb;
    public void getConnection(GameObject electronicBlock, Boolean recursive)
    {

        //print("FF:" + listOfParts[electronicBlock.GetComponent<NumberStorage>().storage].forwardFactor);
        //print("TYPE:" + listOfParts[electronicBlock.GetComponent<NumberStorage>().storage].GetType());

        forward =
            electronicBlock.transform.forward
            * listOfParts[electronicBlock.GetComponent<NumberStorage>().storage].forwardFactor
            + electronicBlock.transform.position;

        Collider[] intersecting = Physics.OverlapSphere(forward, 0.01f);
        if (intersecting.Length > 0)
        {
            if (intersecting[0].gameObject != electronicBlock)
            {
                electronicBlock.GetComponent<GameObjectStorage>().storage = intersecting[0].gameObject;
                addBlock(intersecting[0].gameObject);
                listOfParts[electronicBlock.GetComponent<NumberStorage>().storage].connections[0] = listOfParts[intersecting[0].gameObject.GetComponent<NumberStorage>().storage];


                switch (intersecting[0].gameObject.GetComponent<ElectronicType>().type)
                {
                    case "Motor":
                        break;
                    case "Thruster":
                        break;
                    default:
                        if (recursive == true)
                        {
                            getConnection(intersecting[0].gameObject, true);
                        }
                        break;
                }
                //if (listOfParts[intersecting[0].gameObject.GetComponent<NumberStorage>().storage].propagate == "YES")
                //        {
                //            addBlock(intersecting[0].gameObject);
                //        }

                
            }
        }

    }

    public List<GameObject> orbs = new List<GameObject>();
    public void clearWiring()
    {
        listOfParts.Clear();
        foreach (GameObject orb in orbs)
        {
            Destroy(orb);
        }
    }

    public void addBlock(GameObject electronicBlock)
    {
        print("ID NO WORK:" + electronicBlock.name);
        int ID = electronicBlock.GetComponent<NumberStorage>().storage;

        if (!listOfParts.ContainsKey(ID))
        {
            GameObject newOrb = Instantiate(orb);

            newOrb.transform.parent = electronicBlock.transform;
            newOrb.transform.position = electronicBlock.transform.up * 2 + electronicBlock.transform.position;
            orbs.Add(orb);

            ElectronicBase newBlock = new ElectronicBase();

            switch (electronicBlock.GetComponent<ElectronicType>().type)
            {
                case "AND":
                    newBlock = new AndGate();
                    listOfParts.Add(ID, newBlock);
                    break;
                case "OR":
                    newBlock = new OrGate();
                    listOfParts.Add(ID, newBlock);
                    break;
                case "XOR":
                    newBlock = new XorGate();
                    listOfParts.Add(ID, newBlock);
                    break;
                case "NButton":
                    newBlock = new NButton();
                    listOfParts.Add(ID, newBlock);
                    break;
                case "Wire":
                    newBlock = new Wire();
                    listOfParts.Add(ID, newBlock);
                    break;
                case "Delay":
                    newBlock = new Delayer();
                    listOfParts.Add(ID, newBlock);
                    break;
                case "Resistor":
                    newBlock = new Resistor();
                    listOfParts.Add(ID, newBlock);
                    break;
                case "Motor":
                    newBlock = new Motor();
                    listOfParts.Add(ID, newBlock);
                    break;
                case "Thruster":
                    newBlock = new Thruster();
                    listOfParts.Add(ID, newBlock);
                    break;
            }

            newBlock.Block = electronicBlock;
            newBlock.WiringScript = this;
            newBlock.Orb = newOrb;
        }
    }
    public void doTick()
    {
            //origin.Action();
        foreach (KeyValuePair<int, ElectronicBase> electronic in listOfParts)
        {
            print(electronic.Value.Power);
            electronic.Value.Action();
        }
    }

    public bool GKD(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            print("GKDyes");
            return true;
        }
        print("GKDnope");
        return false;
    }
    public void compile(GameObject originBlock)
    {
        addBlock(originBlock);
        getConnection(originBlock, true);
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 0.5f)
        {
            timePassed = 0f;
            doTick();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BodyCreation : MonoBehaviour
{

    public Creation creation;


    public Grouping grouping;


    public GameObject BodiesContent;

    public GameObject BodyButtonCopy;

    public List<List<GameObject>> bodies = new List<List<GameObject>>
    {

    };

    public List<GameObject> usedUpBlocks = new List<GameObject>
    {

    };


    public List<List<GameObject>> motorsList = new List<List<GameObject>>
    {

    };

    public List<GameObject> turningMotorList = new List<GameObject>
    {

    };
    public List<GameObject> bodyParts = new List<GameObject>
    {

    };
    enum motorDirection
    {
        TOWARD,
        BACKWARD,
        NOT_FACING
    };

    public List<GameObject> findAdjacents(GameObject block)
    {
        Vector3 position = block.transform.position;

        float fullX = block.GetComponent<BoxCollider>().size.x;

        float fullY = block.GetComponent<BoxCollider>().size.y;
        float fullZ = block.GetComponent<BoxCollider>().size.z;



        print("COLLIDER SIZE:" + fullX + ", " + fullY + ", " + fullZ + ", ");
        print("POSITION:" + position);
        //Vector3 left = position + new Vector3(x + 0.5f, 0f, 0f);
        //Vector3 right = position + new Vector3(-x - 0.5f, 0f, 0f);
        //Vector3 front = position + new Vector3(y + 0.5f, 0f, 0f);
        //Vector3 back = position + new Vector3(-y - 0.5f, 0f, 0f);
        //Vector3 top = position + new Vector3(z + 0.5f, 0f, 0f);
        //Vector3 bottom = position + new Vector3(-z - 0.5f, 0f, 0f);

        //Vector3[] directions = new Vector3[6] { left, right, front, back, top, bottom };

        List<GameObject> adjacents = new List<GameObject>();

        float startingX = position.x - (fullX / 2) - 0.5f;
        float endingX = position.x + (fullX / 2) + 0.5f;


        float startingY = position.y - (fullY / 2) - 0.5f;
        float endingY = position.y + (fullY / 2) + 0.5f;

        float startingZ = position.z - (fullZ / 2) - 0.5f;
        float endingZ = position.z + (fullZ / 2) + 0.5f;


        for (float zi = startingZ + 1f; zi < endingZ; zi++)
        {
            for (float xi = startingX + 1f; xi < endingX; xi++)
            {
                Vector3 checkedPosition1 = new Vector3(xi, startingY, zi);
                Vector3 checkedPosition2 = new Vector3(xi, endingY, zi);

               

                print("X CHECKEDPOSITION1:" + checkedPosition1 + position);
                print("X CHECKEDPOSITION2:" + checkedPosition2 + position);

                Collider[] intersecting = Physics.OverlapSphere(checkedPosition1, 0.01f);

                if (intersecting.Length > 0)
                {
                    print("FOUND ADJ");
                    if (!adjacents.Contains(intersecting[0].gameObject))
                    {
                        adjacents.Add(intersecting[0].gameObject);
                    }
                    //block.GetComponent<ListStorage>().storage.Add(intersecting[0].gameObject);

                }

                Collider[] intersecting2 = Physics.OverlapSphere(checkedPosition2, 0.01f);

                if (intersecting2.Length > 0)
                {
                    print("FOUND ADJ");

                    if (!adjacents.Contains(intersecting2[0].gameObject))
                    {
                        adjacents.Add(intersecting2[0].gameObject);
                    }
                    //block.GetComponent<ListStorage>().storage.Add(intersecting2[0].gameObject);

                }

            }
            for (float yi = startingY + 1f; yi < endingY; yi++)
            {
                Vector3 checkedPosition1 = new Vector3(startingX, yi, zi);
                Vector3 checkedPosition2 = new Vector3(endingX, yi, zi);

           

                print("Y CHECKEDPOSITION1:" + checkedPosition1 + position);
                print("Y CHECKEDPOSITION2:" + checkedPosition2 + position);

                Collider[] intersecting = Physics.OverlapSphere(checkedPosition1, 0.01f);

                if (intersecting.Length > 0)
                {
                    print("FOUND ADJ");
                    if (!adjacents.Contains(intersecting[0].gameObject))
                    {
                        adjacents.Add(intersecting[0].gameObject);
                    }
                    //block.GetComponent<ListStorage>().storage.Add(intersecting[0].gameObject);

                }

                Collider[] intersecting2 = Physics.OverlapSphere(checkedPosition2, 0.01f);

                if (intersecting2.Length > 0)
                {
                    print("FOUND ADJ");
                    if (!adjacents.Contains(intersecting2[0].gameObject))
                    {
                        adjacents.Add(intersecting2[0].gameObject);
                    }
                    //block.GetComponent<ListStorage>().storage.Add(intersecting2[0].gameObject);
                }
            }
        }


        for (float xi = startingX + 1f; xi < endingX; xi++)
        {
            for (float yi = startingY + 1f; yi < endingY; yi++)
            {
                Vector3 checkedPosition1 = new Vector3(xi, yi, startingZ);
                Vector3 checkedPosition2 = new Vector3(xi, yi, endingZ);



                print("X CHECKEDPOSITION1:" + checkedPosition1 + position);
                print("X CHECKEDPOSITION2:" + checkedPosition2 + position);

                Collider[] intersecting = Physics.OverlapSphere(checkedPosition1, 0.01f);

                if (intersecting.Length > 0)
                {
                    print("FOUND ADJ");
                    if (!adjacents.Contains(intersecting[0].gameObject))
                    {
                        adjacents.Add(intersecting[0].gameObject);
                    }
                    //block.GetComponent<ListStorage>().storage.Add(intersecting[0].gameObject);

                }

                Collider[] intersecting2 = Physics.OverlapSphere(checkedPosition2, 0.01f);

                if (intersecting2.Length > 0)
                {
                    print("FOUND ADJ");

                    if (!adjacents.Contains(intersecting2[0].gameObject))
                    {
                        adjacents.Add(intersecting2[0].gameObject);
                    }
                    //block.GetComponent<ListStorage>().storage.Add(intersecting2[0].gameObject);

                }

            }
        }

        //for (int i = 0; i < 7; i++)
        //{
        //    Collider[] intersecting = Physics.OverlapSphere(directions[i], 0.01f);
        //    if (intersecting.Length > 0)
        //    {
        //        adjacents[i] = intersecting[0].gameObject;
        //    }
        //}
        print("adj" + adjacents.Count);

        return adjacents;

    }



    public void initiate()
    {
        StartCoroutine(makeAllBodies());
    }
    IEnumerator makeAllBodies()
    {

        breakBodies();

        foreach (GameObject block in creation.everyObject)
        {
            yield return new WaitForSeconds(0.1f);

            if (block.GetComponent<ElectronicType>())
            {
                if (block.GetComponent<ElectronicType>().type == "Motor")
                {

                    continue;
                }
            }

            if (!usedUpBlocks.Contains(block))
            {
                print("oia1");
                yield return createBody(block);
            }
        }

        for (int i = 0; i < bodies.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);

            print("coiunt greater");
            int bodiesIndex = i + 1;
            grouping.HolderCreator(bodies[i], motorsList[i], turningMotorList[i], true);

            GameObject ConfiguredPanel = Instantiate(BodyButtonCopy.gameObject);
            ConfiguredPanel.GetComponent<NumberStorage>().storage = bodiesIndex;
            //BodyButtonCopy.GetComponentInChildren<UnityEngine.UI.Text>().text = ("Body " + bodiesIndex);

            GameObject textObject = ConfiguredPanel.transform.GetChild(0).gameObject;
            textObject.GetComponent<TMP_Text>().text = "Body " + bodiesIndex;

            ConfiguredPanel.transform.parent = BodiesContent.transform;
        }
        
        
        foreach (GameObject bodyPart in bodyParts)
        {
            yield return new WaitForSeconds(0.1f);

            GameObject turningMotor = bodyPart.GetComponent<GameObjectStorage>().storage;

            if (turningMotor != null)
            {
                bodyPart.transform.SetParent(turningMotor.transform.GetChild(0).gameObject.GetComponent<GameObjectStorage>().storage.transform);
            }
        }

    }

    motorDirection MotorFacing(GameObject motor, GameObject block)
    {
        print("hoboi" + motor.GetComponent<NumberStorage>().storage);
        Collider[] intersecting = Physics.OverlapSphere(motor.transform.position + motor.transform.forward, 0.01f);
        Collider[] intersecting2 = Physics.OverlapSphere(motor.transform.position + (motor.transform.forward * -1), 0.01f);

        print(motor.transform.position + motor.transform.forward + "in1");
        print(motor.transform.position + (motor.transform.forward * -1) + "in2");

        if (intersecting.Length > 0)
        {
            if (intersecting[0].gameObject == block)
            {
                print("INTERSECTION1" + intersecting[0].gameObject.name);
                return motorDirection.TOWARD;
            }
        }


        if (intersecting2.Length > 0)
        {
            if (intersecting2[0].gameObject == block)
            {
                print("INTERSECTION2" + intersecting2[0].gameObject.name);
                return motorDirection.BACKWARD;
            }
        }

        print("WOMP WOMP!!");

        return motorDirection.NOT_FACING;
    }

    public void breakBodies()
    {

        foreach (GameObject body in bodyParts)
        {
            grouping.DestroyGroupHolder(body);
        }
        bodyParts.Clear();
        bodies.Clear();
        motorsList.Clear();
        turningMotorList.Clear();

        //foreach (List<GameObject> body in bodies)
        //{
        //    bodies.Remove(body);
        //}

        foreach (Transform groupchild in BodiesContent.GetComponentsInChildren<Transform>())
        {
            if (groupchild.tag == "Body")
            {
                Destroy(groupchild.gameObject);
            }
        }

    }
    IEnumerator createBody(GameObject origin)
    {
                        print("oia2");
        GameObject turningMotor = null;

        List<GameObject> motors = new List<GameObject>
        {

        };

        List<GameObject> body = new List<GameObject>
        {
            origin
        };

        List<GameObject> queue = new List<GameObject>
        {
            origin
        };

        List<GameObject> checkedBlocks = new List<GameObject>
        {

        };


        while (true)
        {

            yield return new WaitForSeconds(0.1f);

            if (queue.Count == 0)
            {
                break;
            }

            //if (findAdjacents(queue[0]).Count == 0)
            //{
            //    break;
            //}

            foreach (GameObject adjacent in findAdjacents(queue[0]))
            {
                if (!queue.Contains(adjacent) && !checkedBlocks.Contains(adjacent))
                {
                    if (adjacent.GetComponent<ElectronicType>())
                    {
                        if (adjacent.GetComponent<ElectronicType>().type == "Motor")
                        {  
                            print("addedmotor-1");
                            switch (MotorFacing(adjacent, queue[0]))
                            {

                                case motorDirection.TOWARD:
                                    print("addedmotor");
                                    turningMotor = adjacent;

                                    break;
                                case motorDirection.BACKWARD:

                                    print("addedmotor3");
                                    motors.Add(adjacent);
                                    body.Add(adjacent);
                                    //queue.Add(adjacent);
                                    break;
                            }
                        }
                        else
                        {
                            if (!usedUpBlocks.Contains(adjacent))
                            {
                                queue.Add(adjacent);
                                body.Add(adjacent);
                            }

                            

                        }
                    }
                    else
                    {
                        if (!usedUpBlocks.Contains(adjacent))
                        {
                            queue.Add(adjacent);
                            body.Add(adjacent);
                        }
                    }
                }
            }
            checkedBlocks.Add(queue[0]);
            queue.Remove(queue[0]);
        }

        print("body" + body.Count);

        if (body.Count() > 1)
        {
            print("oia3");
            bodies.Add(new List<GameObject>(body));
            motorsList.Add(new List<GameObject>(motors));
            turningMotorList.Add(turningMotor);
            foreach (GameObject usedupBlock in body)
            {
                usedUpBlocks.Add(usedupBlock);
            }
        }
        else
        {
            usedUpBlocks.Add(origin);
        }




        //foreach (GameObject bodyPart in body)
            //{
            //print("GROUPED ADJ");
            //}
            print("DONE ADJ");
    }
    


}


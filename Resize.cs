using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;


public class Resize : MonoBehaviour
{
    public GameObject Creation;

    public GameObject resizedObject;

    Vector3 resizedSize;
    public Settings settings;
    public GameObject handle;

    public GameObject handlercopy;
    public bool MoveGrab = false;

    public float increase;

    public float halfsize;

    float diffX;
    float diffY;

    Vector2 newDragPosition;
    Vector2 oldDragPosition;

    float mousechangex;
    float mousechangey;

    bool top;

    bool side;


    public LayerMask layerMask;

    public List<GameObject> handlelist;

    Vector3 handletransformposition;


    public float ModifyDist(float number)
    {
        float result = 0.00f;
        if (number <= -0.05f)
        {
            result = 1f;
        }
        if (number >= 0.05f)
        {
            result = 1f;
        }
        if (number >= -0.05f & number <= 0.05f)
        {
            result = 0f;
        }
        return result;
    }

    public void UpdateHandles()
    {
        foreach (var hand in handlelist)
        {
            if (hand != handle | handle == null)
            {

                Vector3 type2 = hand.GetComponent<HandlerValues>().type;
                //Vector3 typepositive2 = new Vector3(Mathf.Abs(type2.x), Mathf.Abs(type2.y), Mathf.Abs(type2.z));
                GameObject theobject = hand.GetComponent<HandlerValues>().parent.gameObject;
                //GameObject theobject = hand.GetComponent<HandlerValues>().parent.transform.GetChild(0).gameObject;

                Vector3 poopy = theobject.transform.position;

                Vector3 boopy = theobject.GetComponent<BoxCollider>().bounds.size;

                hand.transform.position = new Vector3(poopy.x + (0.5f * boopy.x * type2.x) + (1 * type2.x), poopy.y + (0.5f * boopy.y * type2.y) + (1 * type2.y), poopy.z + (0.5f * boopy.z * type2.z) + (1 * type2.z));
            }



        }
    }

    public void ClearHandles()
    {
        foreach (var pee in handlelist)
        {
            print(pee);
            Destroy(pee);
        }

        handlelist.Clear();

    }

    
    public void HandleCreator(GameObject objecto)
    {
        settings.Grabbing = false;
        print("stew that");

        

        print("The objecto?" + objecto.name);

        Vector3 possy = objecto.transform.position;





        Vector3 bossy = objecto.transform.GetComponent<BoxCollider>().bounds.size;

        //seperated!

        ClearHandles();


        GameObject posX = Instantiate(handlercopy);

        posX.GetComponent<HandlerValues>().type = new Vector3(1, 0, 0);

        posX.transform.position = new Vector3(possy.x + (0.5f * bossy.x) + 1, possy.y, possy.z);
        print(possy + "possy");

        posX.GetComponent<HandlerValues>().parent = objecto;
        posX.GetComponent<HandlerValues>().PosToParent = (objecto.GetComponent<Collider>().bounds.size.x * 0.5f);

        posX.GetComponent<HandlerValues>().Dist = Vector3.Scale(objecto.transform.InverseTransformPoint(posX.transform.position), new Vector3(2f / 3f, 2f / 3f, 2f / 3f));


        posX.GetComponent<HandlerValues>().BeginningPos = posX.transform.position;
        print(posX.GetComponent<HandlerValues>().Dist + "fizz");
        print(posX.transform.position + "poo");
        print(objecto.transform.rotation.eulerAngles.y);
        print(posX.transform.position + "poo");

        handlelist.Add(posX);




        GameObject negX = Instantiate(handlercopy);

        negX.GetComponent<HandlerValues>().type = new Vector3(-1, 0, 0);

        negX.transform.position = new Vector3(possy.x - (0.5f * bossy.x) - 1, possy.y, possy.z);

        negX.GetComponent<HandlerValues>().Dist = Vector3.Scale(objecto.transform.InverseTransformPoint(negX.transform.position), new Vector3(2f / 3f, 2f / 3f, 2f / 3f));

        negX.GetComponent<HandlerValues>().parent = objecto;
        negX.GetComponent<HandlerValues>().PosToParent = (objecto.GetComponent<Collider>().bounds.size.x * 0.5f);
        negX.GetComponent<HandlerValues>().BeginningPos = negX.transform.position;

        handlelist.Add(negX);


        //seperated!



        GameObject posY = Instantiate(handlercopy);

        posY.GetComponent<HandlerValues>().type = new Vector3(0, 1, 0);

        posY.transform.position = new Vector3(possy.x, possy.y + (0.5f * bossy.y) + 1, possy.z);

        posY.GetComponent<HandlerValues>().Dist = Vector3.Scale(objecto.transform.InverseTransformPoint(posY.transform.position), new Vector3(2f / 3f, 2f / 3f, 2f / 3f));

        posY.GetComponent<HandlerValues>().parent = objecto;
        posY.GetComponent<HandlerValues>().PosToParent = (objecto.GetComponent<Collider>().bounds.size.y * 0.5f);
        posY.GetComponent<HandlerValues>().type = new Vector3(0, 1, 0);
        posY.GetComponent<HandlerValues>().BeginningPos = posY.transform.position;

        handlelist.Add(posY);


        GameObject negY = Instantiate(handlercopy);

        negY.GetComponent<HandlerValues>().type = new Vector3(0, -1, 0);

        negY.transform.position = new Vector3(possy.x, possy.y - (0.5f * bossy.y) - 1, possy.z);


        negY.GetComponent<HandlerValues>().parent = objecto;
        negY.GetComponent<HandlerValues>().PosToParent = (objecto.GetComponent<Collider>().bounds.size.y * 0.5f);
        negY.GetComponent<HandlerValues>().type = new Vector3(0, -1, 0);
        negY.GetComponent<HandlerValues>().BeginningPos = negY.transform.position;

        negY.GetComponent<HandlerValues>().Dist = Vector3.Scale(objecto.transform.InverseTransformPoint(negY.transform.position), new Vector3(2f / 3f, 2f / 3f, 2f / 3f));


        handlelist.Add(negY);



        //seperation!



        GameObject posZ = Instantiate(handlercopy);

        posZ.GetComponent<HandlerValues>().type = new Vector3(0, 0, 1);

        posZ.transform.position = new Vector3(possy.x, possy.y, possy.z + (0.5f * bossy.z) + 1);


        posZ.GetComponent<HandlerValues>().parent = objecto;
        posZ.GetComponent<HandlerValues>().PosToParent = (objecto.GetComponent<Collider>().bounds.size.z * 0.5f);
        posZ.GetComponent<HandlerValues>().type = new Vector3(0, 0, 1);
        posZ.GetComponent<HandlerValues>().BeginningPos = posZ.transform.position;

        posZ.GetComponent<HandlerValues>().Dist = Vector3.Scale(objecto.transform.InverseTransformPoint(posZ.transform.position), new Vector3(2f / 3f, 2f / 3f, 2f / 3f));



        handlelist.Add(posZ);



        GameObject negZ = Instantiate(handlercopy);

        negZ.GetComponent<HandlerValues>().type = new Vector3(0, 0, -1);



        negZ.transform.position = new Vector3(possy.x, possy.y, possy.z - (0.5f * bossy.z) - 1);

        negZ.GetComponent<HandlerValues>().parent = objecto;
        negZ.GetComponent<HandlerValues>().PosToParent = (objecto.GetComponent<Collider>().bounds.size.z * 0.5f);
        negZ.GetComponent<HandlerValues>().type = new Vector3(0, 0, -1);
        negZ.GetComponent<HandlerValues>().BeginningPos = posZ.transform.position;

        negZ.GetComponent<HandlerValues>().Dist = Vector3.Scale(objecto.transform.InverseTransformPoint(negZ.transform.position), new Vector3(2f / 3f, 2f / 3f, 2f / 3f));


        handlelist.Add(negZ);

        UpdateHandles();





    }
    public void Update()
    {


        Ray rayb = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        oldDragPosition = newDragPosition;
        newDragPosition = Input.mousePosition;

        mousechangex = newDragPosition.x - oldDragPosition.x;

        mousechangey = newDragPosition.y - oldDragPosition.y;

        if (Input.GetMouseButtonDown(0))
        {// CLICK
            if (Physics.Raycast(rayb, out hit, 1000, ~layerMask))
            {

                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground")) // Click  on  a handle
                {

                    if (settings.ResizeOn == true || settings.MoveOn == true)
                    {
                        settings.Grabbing = true;
                    }
                    //if (MoveOn == true)
                    //{
                    //    MoveGrab = true;
                    //}
                    handle = hit.transform.gameObject;
                    print("I grabbed a resize cuh");
                    halfsize = handle.GetComponent<HandlerValues>().PosToParent;
                    print(halfsize + "halfsize");
                }
                else
                {
                    settings.Grabbing = false;
                }

                if ((settings.ResizeOn == true || settings.MoveOn == true) && settings.Grabbing == false && EventSystem.current.IsPointerOverGameObject() == false) // click on block with resize on 
                {
                    HandleCreator(hit.collider.gameObject);
                }
            }
            else
            {
                settings.Grabbing = false;
                ClearHandles();
            }
            
        }
        

        if (handle != null)
        {

            GameObject handleobject = handle.GetComponent<HandlerValues>().parent;

            if (handleobject != null) // might cause a billion problems
            {
                Vector3 type = handle.GetComponent<HandlerValues>().type;

                Vector3 typepositive = new Vector3(ModifyDist(type.x), ModifyDist(type.y), ModifyDist(type.z));

                Vector3 ones = new Vector3(1, 1, 1);

                Vector3 dist = handle.GetComponent<HandlerValues>().Dist;
                //Vector3 distpositive = new Vector3(Sign(distance.x), Sign(distance.y), Sign(distance.z));
                float dpx = ModifyDist(dist.x);
                float dpy = ModifyDist(dist.y);
                float dpz = ModifyDist(dist.z);

                Vector3 distpositive = new Vector3(dpx, dpy, dpz);

                print("Distance" + handle.GetComponent<HandlerValues>().Dist);
                print("Dist" + distpositive);


                Vector3 objPosition = Camera.main.WorldToScreenPoint(handleobject.transform.position);
                Vector3 handlePosition = Camera.main.WorldToScreenPoint(handle.transform.position);

                diffX = objPosition.x - handlePosition.x;
                diffY = objPosition.y - handlePosition.y;

                // print("The diff is " + diffY);
                // Checking what side

                if (diffY >= 0)
                {
                    top = true; //top
                    print("top");
                }
                else
                {
                    top = false; //bottom
                    print("bottom");

                }

                if (diffX >= 0)
                {
                    side = true; // left
                    print("left");

                }
                else
                {
                    side = false; //right
                    print("right");

                }


                // Hypotheticals.
                // the bug is that the x axis doesnt work. blocked by 

                if (side == true) //left
                {
                    handletransformposition = handle.transform.position - (handle.GetComponent<HandlerValues>().type * (mousechangex * 0.01f));
                    // if handle is to the left of the object, move according to mouse

                }
                else //right
                {
                    handletransformposition = handle.transform.position + (handle.GetComponent<HandlerValues>().type * (mousechangex * 0.01f));
                    // if handle is to the right of the object, move inverted according to mouse



                }


                if (top == true)
                {
                    handletransformposition = handletransformposition - (handle.GetComponent<HandlerValues>().type * (mousechangey * 0.01f));

                }
                else
                {
                    handletransformposition = handletransformposition + (handle.GetComponent<HandlerValues>().type * (mousechangey * 0.01f));

                }

                //testincrease = (handleobject.transform.position - handle.transform.position).magnitude - halfsize;
                /////////////////////////////////////////
                ///////////////////////////////////////////
                //////////////////////////////////////////.
            
                ////////////////////////////////////////////////////////////////////////////////////////
    


                if (settings.MoveOn == true)
                {
                    handle.transform.position = handletransformposition;

                    increase = (handleobject.transform.position - handle.transform.position).magnitude - halfsize;

                    if (increase > 2)
                    {
                        handleobject.transform.position = handleobject.transform.position + type;


                        UpdateHandles();
                    }
                    if (increase < 1)
                    {

                        handleobject.transform.position = handleobject.transform.position - type;

                        UpdateHandles();
                    }
                    print(halfsize);
                }

                if (settings.ResizeOn == true)
                {
                    print("NANANA");

                    if ((handleobject.transform.localScale.x <= typepositive.x | handleobject.transform.localScale.y <= typepositive.y | handleobject.transform.localScale.z <= typepositive.z) == true) //If the object is at the smallest size
                    {
                        if ((handletransformposition - handleobject.transform.position).magnitude - 0.5 >= 1) //And the handle isn't moving back
                        {
                            handle.transform.position = handletransformposition;
                        }
                    }
                    else if ((handleobject.transform.localScale.x <= typepositive.x | handleobject.transform.localScale.y <= typepositive.y | handleobject.transform.localScale.z <= typepositive.z) == false) // if the object isn't at the smallest size
                    {
                        handle.transform.position = handletransformposition;
                    }


                    if (typepositive.x == 1)
                    {
                        halfsize = handleobject.GetComponent<Collider>().bounds.size.x * 0.5f;
                    }
                    if (typepositive.y == 1)
                    {
                        halfsize = handleobject.GetComponent<Collider>().bounds.size.y * 0.5f;
                    }
                    if (typepositive.z == 1)
                    {
                        halfsize = handleobject.GetComponent<Collider>().bounds.size.z * 0.5f;
                    }


                    increase = (handleobject.transform.position - handle.transform.position).magnitude - halfsize;
                    print((handleobject.transform.position - handle.transform.position).magnitude + "mag");
                    print(halfsize + "halfsize");
                    print(increase + "increase");
                    //If Increases\




                    if (increase > 2)
                    {


                        handleobject.transform.localScale = handleobject.transform.localScale + distpositive;

                        handleobject.transform.position = handleobject.transform.position + (Vector3.Scale(type, new Vector3(0.5f, 0.5f, 0.5f)));

                        halfsize += 0.5f;

                        //print("Increaser" + Random.Range(0, 70000));
                        UpdateHandles();


                    }
                    if (increase < 1 && (handleobject.GetComponent<Collider>().bounds.size.x <= typepositive.x | handleobject.GetComponent<Collider>().bounds.size.y <= typepositive.y | handleobject.GetComponent<Collider>().bounds.size.z <= typepositive.z) == false)
                    {

                        handleobject.transform.localScale = handleobject.transform.localScale - distpositive;



                        handleobject.transform.position = handleobject.transform.position - (Vector3.Scale(type, new Vector3(0.5f, 0.5f, 0.5f)));

                        halfsize += -0.5f;



                        //print("Increaser" + Random.Range(0, 70000));
                        UpdateHandles();




                    }


                    //handleobjectkid.GetComponent<BoxCollider>().size = handleobject.transform.localScale;


                }
            }
            else
            {
                ClearHandles();
            }
        }

        
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Creation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject tobecopied;

    //public List<GameObject> selected;
    public GameObject[] list;
    public Vector3 origspot;

    public int currentID;
    public List<GameObject> everyWire;
    public Drag drag;
    public Canvas canvas;
    public LayerMask layerMask;

    public Vector3 pos;
    Vector3 oldDragPosition;

    private RaycastHit hit;

    Vector3 newDragPosition;

    float mousechangex;
    float mousechangey;

    float halfsize;

    float diffX;
    float diffY;
    public GameObject Panel;

    public Grouping grouping;

    public Settings settings;
    public GameObject handle;
    public float xceil;

    public GameObject pendingObject;
    public Quaternion orient = Quaternion.Euler(0, 0, 0);

    public float yceil;
    public float zceil;

    public bool side;

    public bool top;

    public List<GameObject> everyObject;
    public List<GameObject> getEveryObject()
    {
        return everyObject;
    }

    public void SelectViaID(int ID)
    {
        GameObject SelectedObject = list[ID];
        SelectObject(SelectedObject);
    }
    public void SelectObject(GameObject SelectedObject)
    {
        grouping.DestroyGroupHolder();
        //PanelToggle();
        pendingObject = Instantiate(SelectedObject, pos, orient);

        pendingObject.name = SelectedObject.name;

        int count = 0;
        foreach (Transform part in pendingObject.GetComponentInChildren<Transform>())
        {
            if (part.gameObject.GetComponent<BoxCollider>())
            {
                count += 1;
                part.gameObject.layer = LayerMask.NameToLayer("pendingObject");
                everyObject.Add(part.gameObject);
            }
        }
        //pendingObject.GetComponent<Renderer>().material.color = Colour;
        if (count == 0)
        {
            pendingObject.layer = LayerMask.NameToLayer("pendingObject");
            everyObject.Add(pendingObject);
        }
        else
        {
            pendingObject.tag = "prefab";
            pendingObject.layer = LayerMask.NameToLayer("pendingObject");
            pendingObject.SetActive(true); 
            grouping.GroupHolder = pendingObject;
        }

        //GameObject origin = pendingObject.transform.Find("Origin").GetComponent<Condition1>().storage;

            //if (origin != null)
            //{
            //    everyCodeBlock.Add(pendingObject);
            //}

    }

    public void TogglePanel()
    {
        Panel.SetActive(!Panel.activeSelf);
    }
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

    
    IEnumerator Bean(GameObject doobj)
    {
        yield return new WaitForSeconds(0.1f);
        doobj.transform.eulerAngles = Vector3.Lerp(doobj.transform.eulerAngles, orient.eulerAngles, 0.1f);
    }
    IEnumerator Plean(Vector3 scale, GameObject doobja, Vector3 originalPosition)
    {

        Vector3 mart = new Vector3(scale.x - doobja.transform.localScale.x, scale.y - doobja.transform.localScale.y, scale.z - doobja.transform.localScale.z);
        while ((mart.magnitude > 0.2))
        {
            mart = new Vector3(scale.x - doobja.transform.localScale.x, scale.y - doobja.transform.localScale.y, scale.z - doobja.transform.localScale.z);
            yield return new WaitForSeconds(0.1f);
            doobja.transform.localScale = Vector3.Lerp(doobja.transform.localScale, scale, 0.9f);
        }
        doobja.transform.localScale = scale;
        grouping.DestroyGroupHolder();

        // if ((doobja.GetComponent<Collider>().bounds.size.x % 2 == 0) && pendingObject.transform.position.x % 1 == 0) // x is even but x position is not a decimal
        //  {
        //     pendingObject.transform.position = new Vector3(pendingObject.transform.position.x - 0.5f, pendingObject.transform.position.y, pendingObject.transform.position.z);
        //  }
        //  if ((doobja.GetComponent<Collider>().bounds.size.y % 2 == 0) && pendingObject.transform.position.y % 1 == 0) // y is even but y position is not a decimal
        //  {
        //       pendingObject.transform.position = new Vector3(pendingObject.transform.position.x, pendingObject.transform.position.y - 0.5f, pendingObject.transform.position.z);
        //   }
        //  if ((doobja.GetComponent<Collider>().bounds.size.z % 2 == 0) && pendingObject.transform.position.z % 1 == 0) // z is even but z position is not a decimal
        //  {
        //      pendingObject.transform.position = new Vector3(pendingObject.transform.position.x, pendingObject.transform.position.y, pendingObject.transform.position.z - 0.5f);
        //  }

        recreateObject(scale, originalPosition);
    }

    public void recreateObject(Vector3 original, Vector3 originalPosition)
    {
        pendingObject.transform.position = originalPosition;
        if (pendingObject.tag == "prefab")
        {
            everyObject.Add(pendingObject);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            print("Im left");
            GameObject thechosenone = pendingObject;
            Quaternion origrot = thechosenone.transform.rotation;
            string naime = thechosenone.name;
            pendingObject = null;

            orient = origrot;

            pendingObject = Instantiate(thechosenone, pos, orient);
            pendingObject.name = naime;
            pendingObject.transform.localScale = original;
            pendingObject.layer = LayerMask.NameToLayer("pendingObject");
            everyObject.Add(pendingObject);
            //foreach (GameObject thing in objects)
            //{
            //  if (thing.name == naime)
            //  {
            //      pendingObject = null;

            //       pendingObject = Instantiate(thing, pos, orient);
            //      pendingObject.name = naime;
            //     pendingObject.transform.localScale = original;
            //      pendingObject.layer = LayerMask.NameToLayer("ObjectLayer");
            //   }
            //}


        }
        else
        {
            pendingObject = null;
        }
    }
    public void PlaceObject()
    {
        Vector3 originalPosition = pendingObject.transform.position;

        currentID += 1;
        pendingObject.GetComponent<NumberStorage>().storage = currentID;
        pendingObject.layer = LayerMask.NameToLayer("Default");
        print(pendingObject.transform.forward);
        pendingObject.transform.eulerAngles = orient.eulerAngles;

        Vector3 original = pendingObject.transform.localScale;

        pendingObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        StartCoroutine(Plean(original, pendingObject, originalPosition));

        

        //else
        //{
        //print("not shifting");
        //pendingObject = null;
        //if (GroupHolder != null)
        //{
        //    DestroyGroupHolder();
        //    print("Destroy C");

        //}

        //}

    }
    void Update()
    {




        if (Input.GetKeyDown(KeyCode.C))
        {
            Ray rayd = Camera.main.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(rayd, out hit, 1000))
            {
                tobecopied = hit.transform.gameObject;

                Destroy(pendingObject);
                pendingObject = null;

                if (Input.GetKey(KeyCode.LeftShift) && grouping.selected.Count != 0)
                {
                    if (grouping.selected.Count == 1)
                    {
                        pendingObject = Instantiate(grouping.selected[0]);
                        pendingObject.name = tobecopied.name;

                    }
                    else
                    {
                        //List<GameObject> newselected = selected;
                        //DestroyGroupHolder();
                        //foreach (GameObject go in newselected)
                        //{
                        //selected.Add(go);
                        //}
                        //HolderCreator(true);                      //Make groupholder pendig object?
                        //pendingObject = GroupHolder;
                        //print("Destroy B");

                    }
                }
                else
                {
                    tobecopied = hit.transform.gameObject;

                    pendingObject = Instantiate(tobecopied);
                    pendingObject.name = tobecopied.name;



                }

                orient = pendingObject.transform.rotation;
                pendingObject.layer = LayerMask.NameToLayer("pendingObject");
                //pendingObject.GetComponent<Renderer>().material.color = Colour;
                everyObject.Add(pendingObject);
            }




        }



        //oldDragPosition = newDragPosition;
        //newDragPosition = Input.mousePosition;
        //mousechangex = newDragPosition.x - oldDragPosition.x;
        //mousechangey = newDragPosition.y - oldDragPosition.y;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    origspot = Input.mousePosition;

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    selectbox.gameObject.SetActive(true);
        //}
        //}


        //if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        //{
        //    othercorner = Input.mousePosition;

        //    selectbox.sizeDelta = new Vector2(Mathf.Abs(origspot.x - othercorner.x), Mathf.Abs(origspot.y - othercorner.y));
        //    selectbox.position = new Vector2((origspot.x + othercorner.x) / 2, (origspot.y + othercorner.y) / 2);
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (selectbox.gameObject.active == true)
        //    {
        //        HolderCreator(true);
        //    }
        //


        pendingObject.transform.position = pos;

        if (settings.DraggingUI == true)
        {
            settings.draggedUI.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(0)) // CLICK
        {
            origspot = Input.mousePosition;



            if (settings.DraggingUI == true)
            {
                settings.DraggingUI = false;
            }

            foreach (RectTransform UI in canvas.GetComponentInChildren<RectTransform>())
            {
                print("ASIDE");
                drag.MouseOver(UI);
            }



            if (pendingObject != null)
            {

                pendingObject.transform.position = pos;


                PlaceObject();

            }

            Ray rayb = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayb, out hit, 1000, ~layerMask))
            {
                print("I hit" + hit.transform.gameObject);


                GameObject hitObject = hit.transform.gameObject;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    grouping.DestroyGroupHolder();
                }
                else
                {
                    if (grouping.selected.Find(p => p = hitObject))
                    {
                        grouping.selected.Remove(hitObject);
                    }
                    else
                    {
                        grouping.selected.Add(hitObject);
                    }
                }





                //else
                //{
                //    ResizeGrab = false;
                //}

                //if (hit.transform.gameObject == null)
                //{
                //    DestroyGroupHolder();
                //}
            }


        }





        if (Input.GetKeyDown(KeyCode.T))
        {
            orient.eulerAngles = orient.eulerAngles + new Vector3(0, 0, 90);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            //orient = orient * Quaternion.Euler(0, 90, 0);

            orient.eulerAngles = orient.eulerAngles + new Vector3(0, 90, 0);



        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //orient = orient * Quaternion.Euler(90, 0, 0);

            orient.eulerAngles = orient.eulerAngles + new Vector3(90, 0, 0);


        }

        if (pendingObject != null)
        {

            Vector3 fart = new Vector3(orient.eulerAngles.x + pendingObject.transform.eulerAngles.x / 2, orient.eulerAngles.y + pendingObject.transform.eulerAngles.y / 2, orient.eulerAngles.z + pendingObject.transform.eulerAngles.z / 2); // average

            float fartadded = (fart.x + fart.y + fart.z);
            print(orient + "fart orient");
            print(pendingObject.transform.rotation + "fart pending");
            print(fartadded + "fartadded");
            if (fartadded > 15)
            {
                StartCoroutine(Bean(pendingObject));
            }
            else
            {
                pendingObject.transform.rotation = orient;

                print(pendingObject.transform.position.x + "aza");
            }

        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10000, ~layerMask3))
        {
            Debug.Log("GAHHa tempos at beginning" + tempos);
            Debug.Log("GAHHa pos at beginning" + pos);



            print("GAHH hit" + hit.point + hit.transform.gameObject);

            Vector3 objPosition = hit.transform.position;

            xceil = 0.5f;
            yceil = 0.5f;
            zceil = 0.5f;


            if (pendingObject != null)
            {
                halfx = 0;
                halfy = 0;
                halfz = 0;
                if ((int)pendingObject.GetComponent<Collider>().bounds.size.x != 1)
                {
                    halfx = (pendingObject.GetComponent<Collider>().bounds.size.x * 0.5f);

                }
                if ((int)pendingObject.GetComponent<Collider>().bounds.size.y != 1)
                {
                    print("hit int" + pendingObject.GetComponent<Collider>().bounds.size.y);
                    halfy = (pendingObject.GetComponent<Collider>().bounds.size.y * 0.5f);

                }
                if ((int)pendingObject.GetComponent<Collider>().bounds.size.z != 1)
                {
                    halfz = (pendingObject.GetComponent<Collider>().bounds.size.z * 0.5f);

                }
            }

            hithalfx = (hit.transform.gameObject.GetComponent<Collider>().bounds.size.x * 0.5f);
            hithalfy = (hit.transform.gameObject.GetComponent<Collider>().bounds.size.y * 0.5f);
            hithalfz = (hit.transform.gameObject.GetComponent<Collider>().bounds.size.z * 0.5f);

            // object size 1
            // 2
            //print(halfx + " " + halfy + " " + halfz + "grr");


            //to find if on top, use halfsize
            // (objPosition.x + hithalfx)

            if ((objPosition.x + hithalfx - 0.05f) <= hit.point.x) // pos to right of edge (edge is 4, pos.x is 5)
            {
                tempos = new Vector3(Mathf.Ceil(hit.point.x + halfx), Mathf.Ceil(hit.point.y), Mathf.Ceil(hit.point.z));
                xceil = -0.5f;

                print("hit xCeilberd");
            }
            if ((objPosition.x - hithalfx + 0.05f) >= hit.point.x) //pos to left of edge (edge is -4, pos is 0)
            {
                tempos = new Vector3(Mathf.Floor(hit.point.x - halfx), Mathf.Floor(hit.point.y), Mathf.Floor(hit.point.z));
                xceil = 0.5f;

                print("hit xFloorberd");
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ((objPosition.z + hithalfz - 0.05f) <= hit.point.z) // player to right
            {
                tempos = new Vector3(Mathf.Ceil(hit.point.x), Mathf.Ceil(hit.point.y), Mathf.Ceil(hit.point.z) + halfz);
                zceil = -0.5f;

                print("hit zCeilberd");

            }
            if ((objPosition.z - hithalfz + 0.05f) >= hit.point.z)
            {
                tempos = new Vector3(Mathf.Floor(hit.point.x), Mathf.Floor(hit.point.y), Mathf.Floor(hit.point.z) - halfz);
                zceil = 0.5f;

                print("hit zFloorberd");

            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ((objPosition.y + hithalfy - 0.05f) <= hit.point.y) // player to bottom
            {
                tempos = new Vector3(Mathf.Ceil(hit.point.x), Mathf.Ceil(hit.point.y) + halfy, Mathf.Ceil(hit.point.z));
                yceil = -0.5f;

                print("hit yCeilberd");


            }
            if ((objPosition.y - hithalfy + 0.05f) >= hit.point.y)
            {
                tempos = new Vector3(Mathf.Floor(hit.point.x), Mathf.Floor(hit.point.y) - halfy, Mathf.Floor(hit.point.z));
                yceil = 0.5f;

                print("hit yFloorberd");


            }
            Debug.Log("GAHHa tempos at middle" + tempos);
            Debug.Log("GAHHa pos at  middle" + pos);

            print("GAHH objposition + hithalfx + y + z " + objPosition + " " + hithalfx + " " + hithalfy + " " + hithalfz);
            print("hit tempos" + tempos);
            print("GAHH !" + pendingObject.GetComponent<Collider>().bounds.size);
            print("GAHH xceil is " + xceil);
            print("GAHH yceil is " + yceil);
            print("GAHH zceil is " + zceil);
            print("GAHH %2" + (decimal)(pendingObject.GetComponent<Collider>().bounds.size.x) % 2m);
            print("GAHH %1" + (decimal)(Mathf.Abs(pos.x)) % 1m);
            print("GAHH Before the rounding is.. " + tempos);

            print("GAHH" + pendingObject.GetComponent<Collider>().bounds.size.x + "bounds" + pos.x + "pos");
            print("GAHH" + pendingObject.GetComponent<Collider>().bounds.size.x % 2f + "rembounds" + pos.x % 1f + "rempos");



            if (((decimal)(pendingObject.GetComponent<Collider>().bounds.size.x) % 2m < 0.001m) && ((decimal)(Mathf.Abs(tempos.x)) % 1m < 0.001m)) // x is even but x position is not a decimal
            {
                print("GAHH xceil is " + xceil);
                print("GAHH pos before rounding is " + pos);
                tempos = new Vector3(tempos.x + xceil, tempos.y, tempos.z);
                print("GAHH pos after rounding is " + pos);

                print("GAHH x even");

            }



            if (((decimal)pendingObject.GetComponent<Collider>().bounds.size.y % 2m < 0.001m) && ((decimal)(Mathf.Abs(tempos.y)) % 1m < 0.001m)) // y is even but y position is not a decimal
            {
                tempos = new Vector3(tempos.x, (tempos.y + yceil), tempos.z);
                print("GAHH y even");

            }

            if (((decimal)pendingObject.GetComponent<Collider>().bounds.size.z % 2m < 0.001m) && ((decimal)(Mathf.Abs(tempos.z)) % 1m < 0.001m)) // z is even but z position is not a decimal
            {
                tempos = new Vector3(tempos.x, tempos.y, tempos.z + zceil);
                print("GAHH z even");

            }
            //////////////////////////////////////////////////////
            if (((decimal)pendingObject.GetComponent<Collider>().bounds.size.x % 2m > 0.001m) && ((decimal)(Mathf.Abs(tempos.x)) % 1m > 0.001m)) // x is odd but x position is  a decimal
            {
                if (xceil == -0.5f)
                {
                    tempos = new Vector3(Mathf.Floor(tempos.x), tempos.y, tempos.z);
                }
                else
                {
                    tempos = new Vector3(Mathf.Ceil(tempos.x), tempos.y, tempos.z);
                }
                print("GAHH x odd");
            }

            if (((decimal)pendingObject.GetComponent<Collider>().bounds.size.y % 2m > 0.001m) && ((decimal)(Mathf.Abs(tempos.y)) % 1m > 0.001m)) // y is odd but y position is  a decimal
            {
                print("GAHH y odd");
                if (yceil == -0.5f)
                {
                    tempos = new Vector3(tempos.x, Mathf.Floor(tempos.y), tempos.z);
                }
                else
                {
                    tempos = new Vector3(tempos.x, Mathf.Ceil(tempos.y), tempos.z);
                }

            }

            if (((decimal)pendingObject.GetComponent<Collider>().bounds.size.z % 2m > 0.001m) && ((decimal)(Mathf.Abs(tempos.z)) % 1m > 0.001m)) // z is odd but z position is  a decimal
            {
                print("GAHH zceil is " + zceil);
                print("GAHH pos before rounding is " + pos);
                tempos = new Vector3(tempos.x, tempos.y, Mathf.Ceil(tempos.z));
                print("GAHH pos after rounding is " + pos);
                print("GAHH z odd");

                if (zceil == -0.5f)
                {
                    tempos = new Vector3(tempos.x, tempos.y, Mathf.Floor(tempos.z));
                }
                else
                {
                    tempos = new Vector3(tempos.x, tempos.y, Mathf.Ceil(tempos.z));
                }

            }
            //if it glitches out, do tempos but make sure that its building off of previous tempos instead of the original pos



            Debug.Log("GAHHa tempos at end" + tempos);
            Debug.Log("GAHHa pos at end" + pos);




            print("GAHH Final pos is... " + pos);

        }

        pos = tempos;

    }

    public LayerMask layerMask3;
    public Vector3 tempos;


    public float halfx = 0f;
    public float halfy = 0f;
    public float halfz = 0f;

    public float hithalfx = 0f;
    public float hithalfy = 0f;
    public float hithalfz = 0f;

    private void FixedUpdate()
    {
        


    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Grouping : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> prefabs;

    public Button addButton;

    public RectTransform selectbox;

    public Button removeButton;

    public Creation creation;


    public BodyCreation bodycreation;
    public Settings settings;

    public List<GameObject> holded;
    public GameObject GroupHolder;

    GameObject leftestX;
    GameObject leftestY;
    GameObject leftestZ;
    GameObject rightestX;
    GameObject rightestY;
    GameObject rightestZ;

    public Resize resize;
    public GameObject groupholdercopy;
    public int selectedSlot;
    public List<GameObject> selected;
    public void addSelectedObject(GameObject selectedObject)
    {
        selected.Add(selectedObject);
    }


    public Vector2 origspot;
    public Vector2 othercorner;

    public void DestroyGroupHolder(GameObject groupholder = null)
    {
        if (groupholder == null)
        {
            groupholder = GroupHolder;
        }

        if (groupholder != null)
        {
            foreach (Transform groupchild in groupholder.GetComponentsInChildren<Transform>())
            {
                groupchild.gameObject.layer = LayerMask.NameToLayer("Default");
                groupchild.SetParent(null, true);
            }
            if (creation.everyObject.Find(p => p = groupholder))
            {
                creation.everyObject.Remove(groupholder);
            }

            Destroy(groupholder);

        }
    }

    public void addSelectedFromBox()
    {
        selected.Clear();

        foreach (GameObject zobj in creation.getEveryObject())
        {
            print("selectbox active2");


            Vector2 zobjPosition = Camera.main.WorldToViewportPoint(zobj.transform.position);

            Vector2 newOrig = Camera.main.ScreenToViewportPoint(origspot);
            Vector2 newOther = Camera.main.ScreenToViewportPoint(othercorner);
            print("selectzobj" + zobjPosition);
            print("selectOrig" + newOrig);
            print("selectOther" + newOther);



            if ((zobjPosition.x < newOrig.x && zobjPosition.x > newOther.x) | (zobjPosition.x > newOrig.x && zobjPosition.x < newOther.x))
            {
                print("selectbox nearkillonsight");


                if ((zobjPosition.y < newOrig.y && zobjPosition.y > newOther.y) | (zobjPosition.y > newOrig.y && zobjPosition.y < newOther.y))
                {

                    selected.Add(zobj);



                }

            }


        }
    }
    public void HolderCreator(List<GameObject> list = null, List<GameObject> motors = null, GameObject turningMotor = null, bool addTo = false)
    {
        print("selectbox active");
        if (GroupHolder != null)
        {
            DestroyGroupHolder();
        }
        GameObject holder = Instantiate(groupholdercopy);
        holder.layer = LayerMask.NameToLayer("Default");
        holder.name = "GroupHolder";


        if (motors != null)
        {
            foreach (GameObject motor in motors)
            {
                print("addedmotor2");

                holder.GetComponent<ListStorage>().storage.Add(((((((((((((((((motor)))))))))))))))));
                //turningMotor.GetComponentInChildren<GameObjectStorage>().storage = holder;

                GameObject cube = motor.transform.Find("Cube").gameObject;
                cube.GetComponent<GameObjectStorage>().storage = holder;
            }
        }
        if (turningMotor != null)
        {
            print("addedmotor4");

            holder.GetComponent<GameObjectStorage>().storage = turningMotor;
            turningMotor.GetComponent<GameObjectStorage>().storage = holder;
        }

        if (addTo == true)
        {
            bodycreation.bodyParts.Add(holder);
        }
        
        if (list == null)
        {
            list = selected;
            GroupHolder = holder;
        }


        
        if (list.Count > 1)
        {
            foreach (GameObject selec in list)
            {
                holded.Add(selec);
                if (holded.Count <= 1)
                {
                    holder.transform.position = selec.transform.position;
                    holder.transform.localScale = selec.transform.localScale;
                    leftestX = selec;
                    rightestX = selec;

                    leftestY = selec;

                    rightestY = selec;

                    leftestZ = selec;
                    rightestZ = selec;

                }
                else
                {
                    if (selec.transform.position.x - (selec.GetComponent<BoxCollider>().bounds.size.x * 0.5) < holder.transform.position.x - (holder.transform.localScale.x * 0.5))
                    {
                        holder.transform.localScale = new Vector3(holder.transform.localScale.x + Mathf.Abs(holder.transform.position.x - selec.transform.position.x), holder.transform.localScale.y, holder.transform.localScale.z);
                        leftestX = selec;
                    }
                    if (selec.transform.position.x + (selec.GetComponent<BoxCollider>().bounds.size.x * 0.5) > holder.transform.position.x + (holder.transform.localScale.x * 0.5))
                    {
                        holder.transform.localScale = new Vector3(holder.transform.localScale.x + Mathf.Abs(holder.transform.position.x - selec.transform.position.x), holder.transform.localScale.y, holder.transform.localScale.z);
                        rightestX = selec;
                    }

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (selec.transform.position.y - (selec.GetComponent<BoxCollider>().bounds.size.y * 0.5) < holder.transform.position.y - (holder.transform.localScale.y * 0.5))
                    {
                        holder.transform.localScale = new Vector3(holder.transform.localScale.x, holder.transform.localScale.y + Mathf.Abs(holder.transform.position.y - selec.transform.position.y), holder.transform.localScale.z);
                        leftestY = selec;

                    }
                    if (selec.transform.position.y + (selec.GetComponent<BoxCollider>().bounds.size.y * 0.5) > holder.transform.position.y + (holder.transform.localScale.y * 0.5))
                    {
                        holder.transform.localScale = new Vector3(holder.transform.localScale.x, holder.transform.localScale.y + Mathf.Abs(holder.transform.position.y - selec.transform.position.y), holder.transform.localScale.z);
                        rightestY = selec;

                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (selec.transform.position.z - (selec.GetComponent<BoxCollider>().bounds.size.z * 0.5) < holder.transform.position.z - (holder.transform.localScale.z * 0.5))
                    {
                        holder.transform.localScale = new Vector3(holder.transform.localScale.x, holder.transform.localScale.y, holder.transform.localScale.z + Mathf.Abs(holder.transform.position.z - selec.transform.position.z));
                        leftestZ = selec;

                    }
                    if (selec.transform.position.z + (selec.GetComponent<BoxCollider>().bounds.size.z * 0.5) > holder.transform.position.z + (holder.transform.localScale.z * 0.5))
                    {
                        holder.transform.localScale = new Vector3(holder.transform.localScale.x, holder.transform.localScale.y, holder.transform.localScale.z + Mathf.Abs(holder.transform.position.z - selec.transform.position.z));
                        rightestZ = selec;

                    }
                }
            }


            float leftXpos = 0;
            float rightXpos = 0;
            float leftYpos = 0;
            float rightYpos = 0;
            float leftZpos = 0;
            float rightZpos = 0;




            if (leftestX == null)
            {
                leftestX = rightestX;
            }
            if (rightestX == null)
            {
                rightestX = leftestX;
            }
            if (leftestY == null)
            {
                leftestY = rightestY;
            }
            if (rightestY == null)
            {
                rightestY = leftestY;
            }
            if (leftestZ == null)
            {
                leftestZ = rightestZ;
            }
            if (rightestZ == null)
            {
                rightestZ = leftestZ;
            }
            ////////////////////////////
            if (leftestX != null)
            {
                leftXpos = leftestX.transform.position.x;
            }
            if (rightestX != null)
            {
                rightXpos = rightestX.transform.position.x;
            }
            if (leftestY != null)
            {
                leftYpos = leftestY.transform.position.y;
            }
            if (rightestY != null)
            {
                rightYpos = rightestY.transform.position.y;
            }
            if (leftestZ != null)
            {
                leftZpos = leftestZ.transform.position.z;
            }
            if (rightestZ != null)
            {
                rightZpos = rightestZ.transform.position.z;
            }

            if (list.Count <= 1)
            {
                Destroy(holder);

            }
            else
            {
                print("SUI");
                holder.transform.position = new Vector3((leftXpos + rightXpos) / 2, (leftYpos + rightYpos) / 2, (leftZpos + rightZpos) / 2);
            }

            foreach (GameObject evil in list)
            {
                evil.transform.parent = holder.transform;
            }
            selectbox.gameObject.SetActive(false);

            holded.Clear();

            list.Clear();

            if (list == selected)
            {
                list.Add(holder); //problems may occur from changingn seelected t list
            }


            if ((settings.MoveOn == true | settings.ResizeOn == true) && GroupHolder == holder)
            {
                print("This handley");
                

                resize.HandleCreator(holder);
            }
        }


    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            origspot = Input.mousePosition;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                selectbox.gameObject.SetActive(true);
            }
        }


        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            othercorner = Input.mousePosition;

            selectbox.sizeDelta = new Vector2(Mathf.Abs(origspot.x - othercorner.x), Mathf.Abs(origspot.y - othercorner.y));
            selectbox.position = new Vector2((origspot.x + othercorner.x) / 2, (origspot.y + othercorner.y) / 2);
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (selectbox.gameObject.activeSelf == true)
            {
                selectbox.gameObject.SetActive(false);
                addSelectedFromBox();
                HolderCreator();
            }
        }
    }

    public Button prefabButton;

    public GameObject currentPrefabButton = null;

    public GameObject prefabList;

    public void addPrefab()
    {
        prefabs.Add(selected[0]);
        GameObject newPrefab = Instantiate(selected[0]);
        newPrefab.SetActive(false);
        Button newPrefabButton = Instantiate(prefabButton);
        newPrefabButton.GetComponent<GameObjectStorage>().storage = newPrefab;
        newPrefabButton.transform.SetParent(prefabList.transform);
        newPrefabButton.onClick.AddListener(() => setCurrentPrefab(newPrefab));
        DestroyGroupHolder();
    }

    public void insertPrefab()
    {
        if (creation.pendingObject != null)
        {
            creation.PlaceObject();
        }
        DestroyGroupHolder();
        //GameObject newPrefab = Instantiate(currentPrefabButton);
        //newPrefab.SetActive(true);

        creation.SelectObject(currentPrefabButton);
        //newPrefab.layer = LayerMask.NameToLayer("pendingObject");
        //newPrefab.tag = "prefab";
        //GroupHolder = newPrefab;
    }
    public void deletePrefab()
    {
        prefabs.Remove(currentPrefabButton);
    }

    void setCurrentPrefab(GameObject currentPrefab)
    {
        print("di");
        currentPrefabButton = currentPrefab;
    }

}


using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Configuration : MonoBehaviour
{
    public GameObject ConfigureContent;
    private RaycastHit hit;
    public Settings settings;
    public LayerMask layerMask;
    void Update()
    {
        Ray rayb = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (settings.ConfigureOn == true)
            {
                if (Physics.Raycast(rayb, out hit, 1000, ~layerMask))
                {
                    if (EventSystem.current.IsPointerOverGameObject() == false)
                    {
                        print("HOLY!!");
                        foreach (Transform groupchild in ConfigureContent.GetComponentsInChildren<Transform>())
                        {
                            if (groupchild.tag == "Configuration")
                            {
                                Destroy(groupchild.gameObject);
                            }
                        }
                        foreach (Transform groupchild in hit.transform.gameObject.GetComponentsInChildren<Transform>())
                        {
                            if (groupchild.tag == "Configuration")
                            {

                                GameObject ConfiguredPanel = Instantiate(groupchild.gameObject);
                                ConfiguredPanel.transform.parent = ConfigureContent.transform;
                            }
                        }
                    }
                }

            }
        }
        // CLICK

    }
    // Start is called before the first frame update

}



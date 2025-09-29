using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public Settings settings;

    public Creation creation;
    public Resize resize;

    private RaycastHit hit;

    public Color Colour = new Color(0.5f, 0.5f, 0.5f, 255);
    public TextMeshProUGUI ColorText;
    public UnityEngine.UI.Image ColorDisplay;




    IEnumerator Colorize(GameObject coloredobject)
    {
        Color objcolor = coloredobject.GetComponent<Renderer>().material.color;
        print(objcolor + "objcolor");

        Color Colou2r = new Color(Colour.r, Colour.g, Colour.b);

        Vector3 martini = new Vector3(Colou2r.r - objcolor.r, Colou2r.g - objcolor.g, Colou2r.b - objcolor.b);
        

        while (martini.magnitude > 0.2)
        {
            yield return new WaitForSeconds(0.1f);
            martini = new Vector3(Colou2r.r - objcolor.r, Colou2r.g - objcolor.g, Colou2r.b - objcolor.b);
            coloredobject.GetComponent<Renderer>().material.color = Color.Lerp(coloredobject.GetComponent<Renderer>().material.color, Colour, 0.5f);
        }
        coloredobject.GetComponent<Renderer>().material.color = Colou2r;
    }

    public void ColorChange(UnityEngine.UI.Slider slider)
    {
        if (slider.name == "red")
        {
            Colour.r = slider.value;
        }
        if (slider.name == "green")
        {
            Colour.g = slider.value;
        }
        if (slider.name == "blue")
        {
            Colour.b = slider.value;
        }
        Colour.a = 1;
        ColorText.text = new string((int)Mathf.Round(Colour.r * 255) + ", " + (int)Mathf.Round(Colour.g * 255) + ", " + (int)Mathf.Round(Colour.b * 255));
        ColorDisplay.color = Colour;
    }

    IEnumerator Destruction(GameObject killedobject)
    {
        Vector3 ones = new Vector3(0.01f, 0.01f, 0.01f);
        Vector3 marty = new Vector3(ones.x - killedobject.transform.localScale.x, ones.y - killedobject.transform.localScale.y, ones.z - killedobject.transform.localScale.z);

        resize.ClearHandles();
        while (marty.magnitude > 0.2)
        {
            yield return new WaitForSeconds(0.1f);
            marty = new Vector3(ones.x - killedobject.transform.localScale.x, ones.y - killedobject.transform.localScale.y, ones.z - killedobject.transform.localScale.z);
            killedobject.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 90), UnityEngine.Random.Range(0, 90), UnityEngine.Random.Range(0, 90));
            killedobject.transform.localScale = Vector3.Lerp(killedobject.transform.localScale, ones, 0.5f);
            print("marty" + marty.magnitude);
        }
        creation.everyObject.Remove(killedobject);
        Destroy(killedobject);

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // CLICK
        //layermasks needed?
        {
            Ray rayb = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (settings.ColorOn == true && Input.GetKey(KeyCode.LeftShift) == false)
            {
                if (Physics.Raycast(rayb, out hit, 1000))
                {
                    if (EventSystem.current.IsPointerOverGameObject() == false)
                    {
                        StartCoroutine(Colorize(hit.transform.gameObject));
                    }
                }
            }

            if (settings.DestroyOn == true && Input.GetKey(KeyCode.LeftShift) == false)
            {
                if (Physics.Raycast(rayb, out hit, 1000))
                {
                    if (EventSystem.current.IsPointerOverGameObject() == false)
                    {
                        StartCoroutine(Destruction(hit.transform.gameObject));
                    }
                }
            }
        }
    }
}
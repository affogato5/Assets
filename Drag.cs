using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    // Start is called before the first frame update
    public Settings settings;
    // Update is called once per frame

    public bool MouseOver(RectTransform UI)
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        float sizeX = UI.sizeDelta.x;
        float sizeY = UI.sizeDelta.y;

        float positionX = UI.position.x;
        float positionY = UI.position.y;

        float left = positionX - (0.5f * x);
        float right = positionX + (0.5f * x);

        float top = positionY + (0.5f * y);
        float bottom = positionY - (0.5f * y);

        if (
            left < x
            && right > x
            && top > y
            && bottom < y
            )
        {
            settings.draggedUI = UI;
            print("INSIDE!!");
            return true;
            
        }
        else
        {
            print("OUTSIDE!!!");
            return false;
        }
    }
    void Update()
    {

    }
}

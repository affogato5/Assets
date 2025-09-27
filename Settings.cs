using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public Boolean ResizeOn;
    public Boolean Grabbing;
    public Boolean MoveOn;

    public Boolean ColorOn;

    public Boolean DestroyOn;

    public Boolean ConfigureOn;

    public bool DraggingUI;

    public Boolean[] settings = new Boolean[5];

    public RectTransform draggedUI;

    public Settings()
    {
        //settings[0] = ConfigureOn;
        settings = new Boolean[5] {ResizeOn, ColorOn, MoveOn, Grabbing, ConfigureOn};
    }

    public void disableAll()
    {
        ResizeOn = false;
        Grabbing = false;
        MoveOn = false;

    }

    public void disableAllExcept(Boolean dih)
    {
        for (int i = 0; i < 6; i++) {
            if (settings[i] != dih)
            {
                settings[i] = false;
            }
        }
    }

    public void toggleResize()
    {
        ResizeOn = !ResizeOn;
        MoveOn = false;
        Grabbing = false;
        DestroyOn = false;
        ConfigureOn = false;
        ColorOn = false;
    }

    public void toggleMove()
    {
        MoveOn = !MoveOn;
        ResizeOn = false;
        Grabbing = false;
        DestroyOn = false;
        ConfigureOn = false;

    }

    public void toggleColor()
    {
        ResizeOn = false;
        MoveOn = false;
        Grabbing = false;
        DestroyOn = false;
        ConfigureOn = false;
        ColorOn = !ColorOn;
    }

    public void toggleDestroy()
    {
        ResizeOn = false;
        MoveOn = false;
        Grabbing = false;
        ColorOn = false;
        ConfigureOn = false;
        DestroyOn = !DestroyOn;
    }

    public void toggleConfigure()
    {
        ResizeOn = false;
        MoveOn = false;
        Grabbing = false;
        ColorOn = false;
        DestroyOn = false;
        ConfigureOn = !ConfigureOn;
    }


}

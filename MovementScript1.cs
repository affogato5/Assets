using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class MovementScript1 : MonoBehaviour
{

    public Transform camera7;
    public Transform player;
    public Transform playerObj;
    public Transform orientation;
    public Camera main;

    

    public float turn = 0.1f;

    public float speed = 1;

    public Rigidbody rigid;

    float turnVelocity;

    bool IsLocked = false;

    // public Vector3 speed = new Vector3(15, 15, 15);
    Vector3 direction = Vector3.forward;


    float rotationX = 0f;
    float rotationY = 0f;
    void Start()
    {
        print("lol");

    }
    Vector3 velocity;

    public void KeyboardInput()
    {

    }

    

    bool useUpdate;
    bool newDeltaObtained;
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.Rotate(1, 0, 0, Space.Self); 
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.main.transform.Rotate(0, 1, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Camera.main.transform.Rotate(-1, 0, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Camera.main.transform.Rotate(0, -1, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.E)) || (Input.GetKey(KeyCode.Q)) || speed > 1)
        {
            bool moving = false;
            if (Input.GetKey(KeyCode.W))
            {
                direction = Vector3.forward;
                moving = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
                moving = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
                moving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction = Vector3.back;
                moving = true;
            }
            if (Input.GetKey(KeyCode.E))
            {
                direction = Vector3.up;
                moving = true;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                direction = Vector3.down;
                moving = true;
            }
            Camera.main.transform.Translate(direction * speed * Time.deltaTime, Space.Self);
            if (speed >= 5)
            {
                speed = 5;
            }
            else
            {
                speed = speed * 1.007f;
            }

            if (moving == false)
            {
                if (speed <= 1)
                {
                    speed = 1;
                }
                else
                {
                    speed = speed * 0.98f;
                }
            }
        }
        else
        {
            if (speed <= 1)
            {
                speed = 1;
            }
            else
            {
                speed = speed * 0.98f;
            }
        }



        //camera7.transform.rotation = Quaternion.LerpUnclamped(camera7.transform.rotation, DirectionTo, 0.9f * Time.deltaTime);

        //Quaternion rot = Quaternion.LookRotation(playerObj.transform.position, Camera.main.ScreenToViewportPoint(Input.mousePosition));

        //playerObj.transform.rotation = Quaternion.Lerp(rot, playerObj.transform.rotation, 0.5f);


    }

}

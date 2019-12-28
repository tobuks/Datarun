﻿using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraControler : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject focusObject;
    private Vector2 focusPosition;
    private float up=12;
    private float down = -3f;
    private float left = -8.5f;
    private float right = 18.5f;
    private float rightLeft = 27f;
    public float followDistance;
    public static bool change = false;

    

    // Update is called once per frame
    void Update()
    {
        focusPosition = focusObject.transform.position;

        //camera up-down
        if (focusPosition.y >= up)
        {
            
            up += 15f;
            down += 15f;
            transform.position += new Vector3(0, 15f, 0);
            //Debug.Log(up);
        }
        if (focusPosition.y < down)
        {
          
            up -= 15f;
            down -= 15f;
            transform.position += new Vector3(0, -15f, 0);
        }
        //camera lef-right
        if (focusPosition.x <= left)
        {
            
            left -= rightLeft;
            right -= rightLeft;
            transform.position += new Vector3(-rightLeft, 0,0);
            
        }
        if (focusPosition.x > right)
        {
           
            left += rightLeft;
            right += rightLeft;
            transform.position += new Vector3(rightLeft, 0,0);
        }

        if (focusPosition.y < 73 && focusPosition.y > 12.5f && focusPosition.x < -145 && focusPosition.x > -166)
        {
            Vector3 distance = focusPosition - (Vector2)transform.position;
            Vector3 moveDistance = Vector2.ClampMagnitude(distance, distance.magnitude - followDistance);
            transform.position+=new Vector3(0,moveDistance.y,0);
            change = true;
        }

        if (change && focusPosition.y > 73)
        {
            change = false;
            transform.position += new Vector3(0, +7.4f, 0);
           
        }
        if (change && focusPosition.y < 12.5)
        {
            change = false;
            transform.position += new Vector3(0, +7.5f, 0);
        }

    }
}

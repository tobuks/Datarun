using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject focusObject;
    private Vector2 focusPosition;
    private float up=12;
    private float down = -3f;
        
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        focusPosition = focusObject.transform.position;

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
    }
}

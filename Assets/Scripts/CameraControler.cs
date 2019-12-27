using UnityEngine;

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

    public static int sceneCount =0;

    // Update is called once per frame
    void Update()
    {
        focusPosition = focusObject.transform.position;

        //camera up-down
        if (focusPosition.y >= up)
        {
            if(sceneCount>10)
            { sceneCount--;}
            else
            { sceneCount++; }
            up += 15f;
            down += 15f;
            transform.position += new Vector3(0, 15f, 0);
            //Debug.Log(up);
        }
        if (focusPosition.y < down)
        {
            if (sceneCount > 10)
            { sceneCount++; }
            else
            { sceneCount--; }
            up -= 15f;
            down -= 15f;
            transform.position += new Vector3(0, -15f, 0);
        }
        //camera lef-right
        if (focusPosition.x <= left)
        {
            sceneCount++;
            left -= rightLeft;
            right -= rightLeft;
            transform.position += new Vector3(-rightLeft, 0,0);
            //Debug.Log(up);
        }
        if (focusPosition.x > right)
        {
            sceneCount--;
            left += rightLeft;
            right += rightLeft;
            transform.position += new Vector3(rightLeft, 0,0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameTimeScript : MonoBehaviour
{

    public static float InGameTime = 0;
    Text igtime;
    // Start is called before the first frame update
    void Start()
    {
        igtime = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        igtime.text = "" + InGameTime;
    }
}

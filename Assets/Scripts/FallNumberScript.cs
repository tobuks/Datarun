using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FallNumberScript : MonoBehaviour
{

    public static int FallNumber = 0;
    Text fllnumber;
    // Start is called before the first frame update
    void Start()
    {
        fllnumber = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        fllnumber.text = "" + FallNumber;
    }
}

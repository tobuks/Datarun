using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpScript : MonoBehaviour
{

    public static int JumpCount = 0;
    Text JumpsNumber;
    // Start is called before the first frame update
    void Start()
    {
        JumpsNumber = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        JumpsNumber.text = ""+ JumpCount;
    }
}

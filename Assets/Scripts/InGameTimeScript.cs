using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameTimeScript : MonoBehaviour
{

    public static float inGameTime = 0;
    Text inGameTimeText;
    // Start is called before the first frame update
    void Start()
    {
        inGameTimeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        inGameTimeText.text = "" + inGameTime;
        
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float tmpT = Time.time - startTime;
        string hours = ((int)tmpT / 3600).ToString("f0");
        string minutes = ((int)tmpT / 60).ToString("f0");
        string seconds = (tmpT % 60).ToString("f1");

        timerText.text = hours + ":" + minutes + ":" + seconds;
        InGameTimeScript.InGameTime = tmpT;
    }
}

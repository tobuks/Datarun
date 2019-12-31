using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI fallNumberText;
    public TextMeshProUGUI jumpNumberText;


    private float startTime;

    public static int fallNumber = 0;
    public static int jumpCount = 0;
    public static float tmpT = 0;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        

    }

    // Update is called once per frame
    void Update()
    {
        tmpT = Time.time - startTime;
        string hours = ((int)tmpT / 3600).ToString("f0");
        string minutes = ((int)tmpT / 60).ToString("f0");
        string seconds = (tmpT % 60).ToString("f0");

        timerText.text ="In-game time: " + hours + ":" + minutes + ":" + seconds;
        jumpNumberText.text = "Jump number: " + jumpCount;
        fallNumberText.text = "Fall number: " + fallNumber;

    }
}

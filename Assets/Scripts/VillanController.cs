using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using System.Diagnostics;

public class VillanController : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    public static bool isAnimation;
    public float timeStart = 11;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        Show_Me_Villain();
    }    

    public void Show_Me_Villain()
    {

        transform.position = new Vector2(4.64f, -1.24f);
        isAnimation = false;
        StartCoroutine(MoveVillanTask());
    }

    IEnumerator MoveVillanTask()
    {
        //yield return new WaitForSeconds(5);//czas scenki to bedzie
        yield return new WaitForSeconds(1f);
        rigidBody.velocity = new Vector2(6f, 16f);
        yield return new WaitForSeconds(2);
        rigidBody.velocity = new Vector2(-6, 16f);
        yield return new WaitForSeconds(2);
        rigidBody.velocity = new Vector2(-6, 16f);
        yield return new WaitForSeconds(2);
        rigidBody.velocity = new Vector2(0, 16f);
        isAnimation = true;
        gameObject.SetActive(false);
    }

}

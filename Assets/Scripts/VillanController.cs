using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class VillanController : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(MoveVillanTask());

    }

    IEnumerator MoveVillanTask()
    {
        //yield return new WaitForSeconds(5);//czas scenki to bedzie
        rigidBody.velocity = new Vector2(6f, 16f);
        yield return new WaitForSeconds(2);
        rigidBody.velocity = new Vector2(-6, 16f);
        yield return new WaitForSeconds(2);
        rigidBody.velocity = new Vector2(-6, 16f);
        yield return new WaitForSeconds(2);
        rigidBody.velocity = new Vector2(0, 16f);

    }

}

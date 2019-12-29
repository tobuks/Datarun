using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    private Rigidbody2D rb;

    public float moveTime;
    public float stopTime;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            rb.velocity = new Vector2(speed, 0);
            yield return new WaitForSeconds(moveTime);
            rb.velocity= new Vector2(0,0);
            yield return  new WaitForSeconds(stopTime);
            rb.velocity = new Vector2(-speed, 0);
            yield return new WaitForSeconds(moveTime);
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(stopTime);
        }
    }

  
}

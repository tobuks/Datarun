using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

/*    public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down,2f);
        if (groundInfo.collider.gameObject.tag != "plat")
        {
            if (movingRight)
            {
                transform.eulerAngles= new Vector3(0,-180,0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles= new Vector3(0,0,0);
                movingRight = true;
            }
        }
    }*/
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
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb.velocity = new Vector2(speed, 0);
            yield return new WaitForSeconds(moveTime);
            rb.velocity= new Vector2(0,0);
            yield return  new WaitForSeconds(stopTime);
            transform.eulerAngles = new Vector3(0, -180, 0);
            rb.velocity = new Vector2(-speed, 0);
            yield return new WaitForSeconds(moveTime);
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(stopTime);
        }
    }

  
}

using System.Collections;
using UnityEngine;


public class FallingObjects : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public GameObject myPrefabs;
    private Vector3 position;
    
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);
        rb.velocity = new Vector2(0, -5);
        Instantiate(myPrefabs, position, Quaternion.identity);
       
    }

    void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, layerMask);
        if (groundInfo.collider)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }

    }
}
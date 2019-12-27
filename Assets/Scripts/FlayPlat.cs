using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlayingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public GameObject myPrefabs;
    private Vector3 position;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
        StartCoroutine(Spawn(position));
    }
    private IEnumerator Spawn(Vector2 position)
    {
        rb.velocity=new Vector2(0,10f);
        yield return new WaitForSeconds(Random.Range(1,5f));
        Instantiate(myPrefabs, position, Quaternion.identity);
       
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.y > 73)
        {
            Destroy(gameObject);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flaying : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public GameObject myPrefabs;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
    }
    private IEnumerator Spawn(Vector2 position)
    {
        yield return new WaitForSeconds(1.9f);
        Instantiate(myPrefabs, position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {

        Destroy(gameObject, 2f);
        StartCoroutine(Spawn(position));
    }

}
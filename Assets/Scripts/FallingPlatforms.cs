﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public GameObject myPrefabs;
    private Collider2D collider;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        collider.enabled=true;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Invoke("DropPlatform",0.5f);//Start function "DropPlatform" 0.5f after collision with player
            var position = transform.position;//remember platform position
            Destroy(gameObject,2f);
            StartCoroutine(Spawn(position));
        }

    }
    //respawn object
    private IEnumerator Spawn(Vector2 position)
    {
        yield return new WaitForSeconds(1.9f);
        Instantiate(myPrefabs, position, Quaternion.identity);

    }
    private void DropPlatform()
    {
        rb.isKinematic = false;
        collider.enabled = false;
    }
}

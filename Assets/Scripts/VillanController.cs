using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using System.Diagnostics;
using System;

public class VillanController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 0.01f;
    public float jumpForce = 3f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    public Stopwatch timer;



    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        rigidBody.velocity = new Vector2(0.5f, 0.5f);
        
    }



    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveVillanTask());

    }
    
    IEnumerator MoveVillanTask()
    {
        yield return new WaitForSeconds(1);
        rigidBody.velocity = new Vector2(1f, 3f);
        yield return new WaitForSeconds(1);
        rigidBody.velocity = new Vector2(-1f, -3f);
        
    }

}

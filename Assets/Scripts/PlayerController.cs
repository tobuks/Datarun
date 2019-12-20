﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 0.01f;
    public float jumpForce = 3f;
    private float rayLength = 0.668f;
    public LayerMask layerMask;
    public bool grounded;
    public GameObject VillainObject;
    public bool canMove;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void GiveUp()
    {
        transform.position = new Vector2(-4f, -1.52f);
        VillainObject.SetActive(true);
        SaveSystem.SavePlayer(this);
    }

    private void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        LoadPlayer();
    }
  
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (isGrounded() && !Input.GetButton("Jump"))
            {
                float moveInput = Input.GetAxis("Horizontal");
                rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
            }
            else if (Input.GetButton("Jump") && isGrounded())
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);

            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            grounded = isGrounded();
            if (grounded && Input.GetButtonUp("Jump"))
            {
                rigidBody.velocity = Vector2.up * jumpForce;
                jumpForce = 3;
                SavePlayer();
            }
            else if (jumpForce < 18f && Input.GetButton("Jump"))
            {
                jumpForce += 0.2f;
            }
        }
        canMove = VillanController.isAnimation;
    }
    bool isGrounded()
    {
        Vector2 boxPos = transform.position + new Vector3(boxCollider.offset.x, boxCollider.offset.y);
        
        Vector2 pos = boxPos - new Vector2(boxCollider.size.x / 2, boxCollider.size.y/2 +0.1f);
        Vector2 posL = boxPos - new Vector2((boxCollider.size.x / 2)-0.02f, 0);
        Vector2 posR = boxPos + new Vector2((boxCollider.size.x / 2)-0.02f, 0);

        Vector2 direction2 = Vector2.down;
        Vector2 direction = Vector2.right;

        RaycastHit2D hitL = Physics2D.Raycast(posL, direction2, rayLength, layerMask);
        RaycastHit2D hitR = Physics2D.Raycast(posR, direction2, rayLength, layerMask);
        RaycastHit2D hit = Physics2D.Raycast(pos, direction, boxCollider.size.x, layerMask);
        Debug.DrawRay(pos, direction, Color.green );
        Debug.DrawRay(posL, direction2, Color.red );
        Debug.DrawRay(posR, direction2, Color.red );
   
     
      return (hitL.collider != null || hitR.collider != null || hit.collider != null );
   

    }
    void OnApplicationQuit()
    {
         SavePlayer();
    }
        

    
}

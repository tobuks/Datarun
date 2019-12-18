using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.01f;
    public float jumpForce = 3f;
    private float rayLength = 0.668f;
    public LayerMask layerMask;

    public bool grounded;

    public bool inWindZone=false;
   [SerializeField] public GameObject windZone;
   [SerializeField] public int timer;


    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    public  float jumpNumber;
    public int fallNumber;
    public float ingameTime = 0;
    public float timeInFall = 0f;
    public float fallCount;
    private bool isFalling;
   
    void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void GiveUp()
    {
        transform.position=new Vector3(4.16f,-1.54f,0);
        SaveSystem.SavePlayer(this);
    }

    void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        jumpNumber = data.position[3];
        
    }
    void Start()
    {
        //start wind system
        StartCoroutine(Wind());

        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        LoadPlayer();
    }
    //wind on and off
    IEnumerator Wind()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            windZone.SetActive(false);
            yield return new WaitForSeconds(timer);
            windZone.SetActive(true);
        }
    }
 
    private void FixedUpdate()
    {
        //player movement right left
        if (isGrounded() && !Input.GetButton("Jump") && !inWindZone)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
        }
        //move block when we hold jump button
        else if(Input.GetButton("Jump") && isGrounded())
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        //player behaviour in wind zone  
        if (inWindZone)
        {
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidBody.AddForce(windZone.GetComponent<WindArea>().direction*windZone.GetComponent<WindArea>().strength);
        }
   
    }
    // Update is called once per frame
    void Update()
    {
        grounded = isGrounded();

        //jump
        if (grounded && Input.GetButtonUp("Jump"))
        {
            rigidBody.velocity = Vector2.up * jumpForce;
            jumpForce = 3;
            jumpNumber++;
            SavePlayer();
        }
        //set jump force 
        else if (jumpForce < 18f && Input.GetButton("Jump") && grounded && (inWindZone==false))
        {
            jumpForce += 0.2f;
        }
       
     
        //falling counter
        if (grounded)
        {
            timeInFall = 0f;
            isFalling = true;
        }
        if (!grounded && rigidBody.velocity.y < 0)
        {
            timeInFall += Time.deltaTime;
        }

        if (timeInFall > 1 && isFalling)
        {
            isFalling = false;
            fallNumber ++;
        }

    }

 

    
    private bool isGrounded()
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

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "windArea")
        {
            windZone = coll.gameObject;
            inWindZone = true;
        }
       
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "windArea")
        {
            inWindZone = false;
        }

    }


    void OnApplicationQuit()
    {
         SavePlayer();
    }
        

    
}

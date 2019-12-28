﻿using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    //floats
    public float speed = 0.01f;
    public float jumpForce = 3f;
    private float rayLength = 0.9f;
    
    //bools
    public bool grounded;
    public bool inWindZone = false;
    public bool inStaticWindZone = false;
    private bool isFalling;
    private bool inDownScene;
    public static bool isRestart=false;

    //serialized fields
    [SerializeField] public GameObject windZone;
    [SerializeField] public int timer;

    //object classes
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    //countery niech one wyleca do menu pause i beda private tutaj one nie sa potrzebne
    public int jumpCount;
    public int fallCount;
    
    public float inGameTime = 0;//co to jest//To jest czas w grze //to go tu nie powinno byc tylko w menu i to nie jest do niczego urzyte wiec 
    //to jest smiec a nie czas w grze

    // Start is called before the first frame update
    void Start()
    {
        //Start when player play first time or restart game
        if (MainMenu.isStart)
        {
            GiveUp();
            MainMenu.isStart = false;
        }
        LoadPlayer();

        //start wind system
        StartCoroutine(Wind());

        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    private void FixedUpdate()
    {
       
        if (!VillanController.isAnimation) return;
       
        // 3 level mechanics 
        if (inDownScene)
        { 
            //player movement right left 
            if (!grounded)
            {
                float moveInput = Input.GetAxis("Horizontal");
                rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
            }
        }
        else
        {
            //player movement right left 
            if (grounded && !Input.GetButton("Jump") && !inWindZone && !inStaticWindZone)
            {
                float moveInput = Input.GetAxis("Horizontal");
                rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
            }
            //move block when we hold jump button 
            else if (Input.GetButton("Jump") && grounded && !inWindZone && !inStaticWindZone)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }

            //set jump force 
            if (jumpForce < 18f && Input.GetButton("Jump") && grounded && !inWindZone && !inStaticWindZone)
            {
                jumpForce += 0.2f;
            }
        }

        //player behaviour in wind zone   
        if (inWindZone)
        {
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidBody.AddForce(windZone.GetComponent<WindArea>().direction *
                               windZone.GetComponent<WindArea>().strength);
        }

        if (inStaticWindZone)
        {
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidBody.AddForce(windZone.GetComponent<WindArea>().direction *
                               windZone.GetComponent<WindArea>().strength);
        }
      

    }
    // Update is called once per frame
    void Update()
    {

        inDownScene = CameraControler.change;
        grounded = IsGrounded();
        if (!VillanController.isAnimation) return;
        

        // physic change in level 3
        if (inDownScene)
        {
            rigidBody.velocity = new Vector2(0,-4f);
            rigidBody.gravityScale = 0.0f;
            
        }

        // back to physic from other levels
        if (!inDownScene)
        {
            rigidBody.gravityScale = 2f;
        }

        //jump 
        if (grounded && Input.GetButtonUp("Jump") && !inDownScene)
        {
            rigidBody.velocity = Vector2.up * jumpForce;
            jumpCount++;
            jumpForce = 3;
            //SavePlayer();
        }
        //falling control
        if (grounded && isFalling)
        {
            isFalling = false;
            fallCount++;
        }
        if (rigidBody.velocity.y < -20 && !inDownScene)
        {
            isFalling = true;
        }

    }
    bool IsGrounded()
    {
        Vector2 boxPos = transform.position + new Vector3(boxCollider.offset.x, boxCollider.offset.y);

        Vector2 pos = boxPos - new Vector2(boxCollider.size.x / 2, boxCollider.size.y / 2 + 0.1f);
        Vector2 posL = boxPos - new Vector2((boxCollider.size.x / 2) - 0.02f, 0);
        Vector2 posR = boxPos + new Vector2((boxCollider.size.x / 2) - 0.02f, 0);

        Vector2 direction2 = Vector2.down;
        Vector2 direction = Vector2.right;

        RaycastHit2D hitL = Physics2D.Raycast(posL, direction2, rayLength, layerMask);
        RaycastHit2D hitR = Physics2D.Raycast(posR, direction2, rayLength, layerMask);
        RaycastHit2D hit = Physics2D.Raycast(pos, direction, boxCollider.size.x, layerMask);
        /*Debug.DrawRay(pos, direction, Color.green);
        Debug.DrawRay(posL, direction2, Color.red);
        Debug.DrawRay(posR, direction2, Color.red);*/

        return (hitL.collider != null || hitR.collider != null || hit.collider != null);
    }
    void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void GiveUp()
    {
        transform.position = new Vector2(-4f, -1.54f);
        isRestart = true;
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

    //wind on and off
    IEnumerator Wind()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            windZone.SetActive(false);
            yield return new WaitForSeconds(timer);
            windZone.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "windArea")
        { 
            inWindZone = true;
        }
        if (coll.gameObject.tag == "staticWindArea")
        {
            inStaticWindZone = true;
        }

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "windArea")
        {
            inWindZone = false;
        }
        if (coll.gameObject.tag == "staticWindArea")
        {
            inStaticWindZone = false;
        }
    }
    void OnApplicationQuit()
    {
        SavePlayer();
    }

}



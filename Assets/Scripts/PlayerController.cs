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
    // Start is called before the first frame update


    //-----------------------zmienne-----------------------------//
    public float speed = 0.01f;
    public float jumpForce = 3f;
    private float rayLength = 0.668f;
    public LayerMask layerMask;
    public bool grounded;
    public bool isright;
    //------------------------------------------------------------//
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void GiveUp()
    {
        transform.position=new Vector3(4.16f,-1.54f,0);
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
        //player move right and left
        if (isGrounded() && !Input.GetButton("Jump"))
        {
            float moveInput = Input.GetAxis("Horizontal");
            rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
        }
        //block player move (right left) when we hold jump key
        else if(Input.GetButton("Jump") && isGrounded())
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
           
        }

        //do obracania
        float horizontal = Input.GetAxis("Horizontal");

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
            SavePlayer();
        }
        //set jump power 
        else if (jumpForce < 18f && Input.GetButton("Jump"))
        {
            jumpForce += 0.2f;
        }

        void Flip(float horizontal)
        {

        }
     
       


    }

    bool isGrounded()
    {
        Vector2 boxPos = transform.position + new Vector3(boxCollider.offset.x, boxCollider.offset.y);
        
        //colider start position
        Vector2 pos = boxPos - new Vector2(boxCollider.size.x / 2, boxCollider.size.y/2 +0.1f);
        Vector2 posL = boxPos - new Vector2((boxCollider.size.x / 2)-0.02f, 0);
        Vector2 posR = boxPos + new Vector2((boxCollider.size.x / 2)-0.02f, 0);

        //collider direction
        Vector2 direction2 = Vector2.down;
        Vector2 direction = Vector2.right;

        //colider hit
        RaycastHit2D hitL = Physics2D.Raycast(posL, direction2, rayLength, layerMask);
        RaycastHit2D hitR = Physics2D.Raycast(posR, direction2, rayLength, layerMask);
        RaycastHit2D hit = Physics2D.Raycast(pos, direction, boxCollider.size.x, layerMask);
        //help lines
        Debug.DrawRay(pos, direction, Color.green );
        Debug.DrawRay(posL, direction2, Color.red );
        Debug.DrawRay(posR, direction2, Color.red );
   
     
      return (hitL.collider != null || hitR.collider != null || hit.collider != null );
   

    }


    
}

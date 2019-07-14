using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerGravity;
    [SerializeField] float runningSpeed = 1f;
    [SerializeField] float climbingSpeed = 1f;
    [SerializeField] float jumpHight = 1f;
    private int jumpCount = 0;

    Vector2 moveDir;

    Animator myAnimtor;
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRighdbody2D;
    Collider2D myCollider;

    private void Start()
    {
        myAnimtor = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRighdbody2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();

        playerGravity = myRighdbody2D.gravityScale;


    }

    private void Update()
    {
        Debug.Log(jumpCount);
        Move();
    }

    private void Move()
    {
        Run();
        Jump();
        Climb();

        
    }

    private void Climb()
    {
        

        if(myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRighdbody2D.gravityScale = 0;
            myRighdbody2D.velocity = new Vector2(0, 0);
            moveDir.y = Input.GetAxis("Vertical");
            moveDir.x = Input.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(moveDir.x * runningSpeed, moveDir.y * climbingSpeed);
            myRighdbody2D.velocity = playerVelocity;

            bool playerVerticalSpeed = Mathf.Abs(moveDir.y) > Mathf.Epsilon;
            myAnimtor.SetBool("climbing", playerVerticalSpeed);
            
        }
        else
        {
            myRighdbody2D.gravityScale = playerGravity;
            myAnimtor.SetBool("climbing", false);
        }

    }

    private void Jump()
    {

        if (Input.GetButtonDown("Jump") &&  jumpCount < 1 )
        {
            myAnimtor.SetBool("jumping", true);
            Vector2 jumpVelocity = new Vector2(0f, jumpHight);
            myRighdbody2D.velocity = new Vector2(0,0);
            myRighdbody2D.velocity += jumpVelocity;
            jumpCount++;
        }

        else if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimtor.SetBool("jumping", false);
            jumpCount = 0;
        }
    }



    private void Run()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(moveDir.x * runningSpeed, myRighdbody2D.velocity.y);
        myRighdbody2D.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(moveDir.x) > Mathf.Epsilon;

        myAnimtor.SetBool("running", playerHorizontalSpeed);
        mySpriteRenderer.flipX = moveDir.x < 0;
      

    }
}

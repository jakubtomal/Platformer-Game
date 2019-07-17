using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float playerGravity;
    [SerializeField] float runningSpeed = 1f;
    [SerializeField] float climbingSpeed = 1f;
    [SerializeField] float jumpHight = 1f;
    private bool canDoubleJump;
    private bool canJump = true;
    [SerializeField] float startHealth = 100f;
    private float health;
    [SerializeField] Image healthBar;
    private bool isAlive = true;

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

        health = startHealth;


    }



    private void Update()
    {
            
        if (!isAlive) { return; }
        Move();
    }

    private void Move()
    {
        IsTounchingGround();
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


        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("click");
            if(canJump)
            {
                Vector2 jumpVelocity = new Vector2(0f, jumpHight);
                myRighdbody2D.velocity = new Vector2(0, 0);
                myRighdbody2D.velocity += jumpVelocity;
                canDoubleJump = true;
                canJump = false;
            }
            else if (canDoubleJump)
            {
                Vector2 jumpVelocity = new Vector2(0f, jumpHight);
                myRighdbody2D.velocity = new Vector2(0, 0);
                myRighdbody2D.velocity += jumpVelocity;
                canDoubleJump = false;
            }  
        }
        else if(IsTounchingGround())
        {
            canJump = true;
        }

        if(IsTounchingGround()){ myAnimtor.SetBool("jumping", false); } 
        else { myAnimtor.SetBool("jumping", true); }



    }

    public bool IsTounchingGround()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Debug.Log(myCollider);
            return true;
        }
        else
        {
            return false;
        }
            
    }


    private void Run()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(moveDir.x * runningSpeed, myRighdbody2D.velocity.y);
        myRighdbody2D.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(moveDir.x) > Mathf.Epsilon;

        myAnimtor.SetBool("running", playerHorizontalSpeed);
        if (moveDir.x != 0 ) { mySpriteRenderer.flipX = moveDir.x < 0; }
        
    }


    public void GetDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount =  health/ startHealth;
        if(health <= 0) { Die(); }
    }

    private void Die()
    {
        if (isAlive) { GetComponent<Animator>().SetBool("Dead", true); }
        isAlive = false;
    }

    
}

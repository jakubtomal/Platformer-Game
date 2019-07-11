using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runningSpeed = 1f;
    [SerializeField] float jumpHight = 1f;

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
        myCollider = GetComponent<CapsuleCollider2D>();


    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Run();
        Jump();

        
    }

    private void Jump()
    {

        if (Input.GetButtonDown("Jump") && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpHight);
            myRighdbody2D.velocity += jumpVelocity;
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

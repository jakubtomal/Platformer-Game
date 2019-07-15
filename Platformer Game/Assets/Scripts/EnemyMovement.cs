using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private SpriteRenderer myspriteRenderer;
    private CapsuleCollider2D myCollider2D;
    private Rigidbody2D myrigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        myspriteRenderer = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<CapsuleCollider2D>();
        myrigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(myCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("Attack");
            Move(0);
            Attack();
        }
        else
        {
            Move(speed);
        }
        
    }


    private void Move(float speed)
    {
        if(IsFacingRight())
        {
            myrigidbody2D.velocity = new Vector2(speed, 0);
        }
        else
        {
            myrigidbody2D.velocity = new Vector2(-speed, 0);
        }
        
    }

    private void Attack()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>()) { return; }
        transform.localScale =  new Vector2( -transform.localScale.x, transform.localScale.y );
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}

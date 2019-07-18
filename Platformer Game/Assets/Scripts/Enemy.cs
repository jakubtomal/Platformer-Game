using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private bool isAttacking = false;

    [SerializeField] float damage = 10;
    private Player currentTarget;

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
        if (!isAttacking) { Move(speed); }
        else { Move(0); }
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

    private void Attack(Player player)
    {
        isAttacking = true;
        GetComponent<Animator>().SetBool("Attacking", true);

        if ((player.transform.position.x < transform.position.x && IsFacingRight()) || (player.transform.position.x > transform.position.x && !IsFacingRight()))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        currentTarget = player;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            isAttacking = false ;
            currentTarget = null;
            GetComponent<Animator>().SetBool("Attacking", false);
            return;
        }
        
        transform.localScale =  new Vector2( -transform.localScale.x, transform.localScale.y );
        
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Attack(collision.GetComponent<Player>());
        }

    }

    private void DealDamage()
    {
        if (currentTarget) { currentTarget.GetDamage(damage); }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSound;
    [SerializeField] int value = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            AudioSource.PlayClipAtPoint(coinPickupSound ,Camera.main.transform.position );
            FindObjectOfType<GameSession>().AddScore(value);
            Destroy(gameObject);
        }
    }
}

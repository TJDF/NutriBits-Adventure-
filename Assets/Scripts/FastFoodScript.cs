using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFoodScript : MonoBehaviour
{    
    public float speed;
    public bool direction;

    Rigidbody2D rb;
    SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        direction = true;
    }


    void Update()
    {
        if (direction)
        {
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
            sr.flipX = true;
        }
        else if (!direction)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            sr.flipX = false;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FastFood")
        {
            direction = !direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Parede")
        {
            direction = !direction;
        }


        if (collision.gameObject.tag == "Invert")
        {
            direction = !direction;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float rangeFollow;
    [SerializeField] float velocity;

    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if (distPlayer > rangeFollow)
        {
            FollowPlayer();

        }
        else 
        {
            NearFollow();
        
        }
    }

    private void FollowPlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        else if (transform.position.x > player.transform.position.x) 
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }

        if (playerRb.velocity == new Vector2(playerRb.velocity.x, 8f))
        {
            rb.velocity = new Vector2(rb.velocity.x, 8f);
            
        }

    }
    private void NearFollow()
    {
        if (playerRb.velocity == new Vector2(playerRb.velocity.x, 8f))
        {
            rb.velocity = new Vector2(rb.velocity.x, 8f);

        }
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    
}

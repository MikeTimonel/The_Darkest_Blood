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
    private Animator mAnimator;
    private Transform mTransform;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerRb = player.GetComponent<Rigidbody2D>();
        mAnimator = gameObject.GetComponent<Animator>();
        mTransform = GetComponent<Transform>();
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
            mAnimator.SetBool("IsWalking", false);
        
        }
        Debug.Log("Velocidad: " + rb.velocity);

    }

    private void FollowPlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            mAnimator.SetBool("IsWalking", true);
            mTransform.eulerAngles = new Vector2(0, 0);

        }
        else if (transform.position.x > player.transform.position.x)
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            mAnimator.SetBool("IsWalking", true);
            mTransform.eulerAngles = new Vector2(0, 180);
        }
        

        if (playerRb.velocity == new Vector2(playerRb.velocity.x, 8f))
        {
            Invoke("jumpCat", 0.25f);

        }

    }
    private void NearFollow()
    {
        if (playerRb.velocity == new Vector2(playerRb.velocity.x, 8f))
        {
            Invoke("jumpCat", 0.25f);

        }
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void jumpCat()
    {
        mAnimator.SetBool("IsWalking", false);
        mAnimator.SetTrigger("Jumping");
        rb.velocity = new Vector2(rb.velocity.x, 8f);

    }

}

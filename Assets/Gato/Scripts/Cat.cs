using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float rangeFollow;
    [SerializeField] private float velocity;

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
    void FixedUpdate()
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
        

    }

    // Cuando el gato esta fuera del area de detección
    private void FollowPlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            CatMovement(velocity, rb.velocity.y, 0);

        }
        else if (transform.position.x > player.transform.position.x)
        {
            CatMovement(-velocity, rb.velocity.y, 180);
        }
        

        if (playerRb.velocity == new Vector2(playerRb.velocity.x, 8f))
        {
            Invoke("CatJump", 0.25f);

        }

    }
    // Cuando el gato esta dentro del radio de detección
    private void NearFollow()
    {
        if (playerRb.velocity == new Vector2(playerRb.velocity.x, 8f))
        {
            Invoke("CatJump", 0.25f);

        }
        rb.velocity = new Vector2(0, rb.velocity.y);

    }
    // Salto del gato
    private void CatJump() {
    
        mAnimator.SetBool("IsWalking", false);
        mAnimator.SetTrigger("Jumping");
        rb.velocity = new Vector2(rb.velocity.x, 8f);

    }

    // Movimiento y rotación del sprite del gato
    private void CatMovement(float vx, float vy, int rotation) {
        rb.velocity = new Vector2(vx, vy);
        mAnimator.SetBool("IsWalking", true);
        mTransform.eulerAngles = new Vector2(0, rotation);
    }
    

}

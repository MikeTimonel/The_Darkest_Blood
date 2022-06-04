using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float velocity;
    [SerializeField] float jump;
    public float life;
    

 


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

   
    private void Movement()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * velocity, rb.velocity.y);
            
        if (Input.GetKeyDown("space")) {
            rb.velocity = new Vector2(rb.velocity.x, jump);
           
        }
       
    }
}

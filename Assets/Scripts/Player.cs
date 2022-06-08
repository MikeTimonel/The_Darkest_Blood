using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float velocity;
    [SerializeField] float jump;
    [SerializeField] float maxHealth;


    public float life;
    

    private PotionCount potions;
    

    

 


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        potions = FindObjectOfType<PotionCount>();
        maxHealth = life;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Healing();

    }

    private void Healing()
    {
        if (Input.GetKeyDown("down") && potions.potions != 0 && life < maxHealth)
        {
            life += 10;
            potions.potions--;
            
        }
    }

    private void Movement()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * velocity, rb.velocity.y);
        

        if (Input.GetKeyDown("space"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            life -= 10;

        }
        
    }
}

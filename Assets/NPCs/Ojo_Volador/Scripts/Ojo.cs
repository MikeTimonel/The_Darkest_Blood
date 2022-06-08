using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ojo : MonoBehaviour
{
    private Rigidbody2D eyeRb;
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float flyDistance;
    [SerializeField] float flyVelocity;
    [SerializeField] private GameObject player;

    private float initPosY;
    private float contador;

    
    
    // Start is called before the first frame update
    void Start()
    {
        eyeRb = GetComponent<Rigidbody2D>();
        initPosY = transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distPlayer < followRange)
        {
            

        }
        else
        {
            Fly();


        }
        

    }

    private void Fly()
    {
        contador += Time.deltaTime * flyVelocity;
        transform.position = new Vector2(transform.position.x, Mathf.PingPong(contador, flyDistance) + initPosY);
    }
}

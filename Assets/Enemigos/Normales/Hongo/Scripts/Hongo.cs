using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hongo : MonoBehaviour
{
    [Header("Rangos")]
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float life;

    [Header("Movimiento")]
    [SerializeField] float velocidadMovimiento;

    [Header("Jugador")]
    [SerializeField] private GameObject player;

    [Header("Collider de ataque")]
    [SerializeField] private GameObject attackObject;
    private BoxCollider2D attackCollider;

    private Rigidbody2D hongoRb;
    private Animator mAnimator;

    private bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        hongoRb = GetComponent<Rigidbody2D>();
        mAnimator = gameObject.GetComponent<Animator>();
        attackCollider = attackObject.GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distPlayer < followRange)
        {
            FollowPlayer();
            AttackPlayer(distPlayer);
            
        }
        else
        {
            Idle();
        }
        
    }

    private void Idle()
    {
        hongoRb.velocity = new Vector2(0, 0);
        mAnimator.SetBool("IsRunning", false);
        mAnimator.SetBool("IsIdle", true);

    }

    private void AttackPlayer(float distPlayer)
    {
        if (distPlayer < attackRange) {
            StartCoroutine("Attack");     
        }
    }

    private void FollowPlayer()
    {
        if (!isAttacking)
        {
            if (transform.position.x > player.transform.position.x)
            {
                Running(-velocidadMovimiento, 180);
            }
            else if (transform.position.x < player.transform.position.x)
            {
                Running(velocidadMovimiento, 0);
            }
        }

    }
    private void Running(float x, float rotation) 
    {
        hongoRb.velocity = new Vector2(x * Time.deltaTime, 0);
        transform.eulerAngles = new Vector2(0, rotation);
        mAnimator.SetBool("IsRunning", true);
        mAnimator.SetBool("IsIdle", false);

    }
    private IEnumerator Attack()
    {
        isAttacking = true;
        mAnimator.SetBool("IsRunning", false);
        mAnimator.SetBool("IsIdle", false);
        hongoRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.5f);
        mAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(1);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.75f);
        attackCollider.enabled = false;
        isAttacking = false;



    }
}

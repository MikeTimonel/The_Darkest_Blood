using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hongo : MonoBehaviour
{
    [Header("Rangos")]
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float vida;
    [SerializeField] public float dañoAtaque;

    [Header("Movimiento")]
    [SerializeField] float velocidadMovimiento;

    [Header("Jugador")]
    [SerializeField] private GameObject player;

    [Header("Collider de ataque")]
    [SerializeField] private GameObject attackObject;
    private BoxCollider2D attackCollider;

    [Header("Sonidos")]
    [SerializeField] private AudioSource reproducir;


    private Rigidbody2D hongoRb;
    private BoxCollider2D hongoCollider;
    private Animator mAnimator;

    private bool isAttacking;
    private bool isDeath;


    // Start is called before the first frame update
    void Start()
    {
        hongoRb = GetComponent<Rigidbody2D>();
        hongoCollider = GetComponent<BoxCollider2D>();
        mAnimator = gameObject.GetComponent<Animator>();
        attackCollider = attackObject.GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
        reproducir.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDeath)
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
        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = false;
        isAttacking = false;



    }

    private IEnumerator Dañado() {
        mAnimator.SetTrigger("Damaged");
        hongoRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
    
    
    }

    private IEnumerator Morir() {
        mAnimator.SetBool("IsDeath", true);
        isDeath = true;
        hongoCollider.enabled = false;
        reproducir.Stop();
        hongoRb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }


    public void TomarDaño(int daño)
    {

        vida -= daño;
        StartCoroutine("Dañado");
        if (vida <= 0)
        {
            StartCoroutine("Morir");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prota"))
        {
            collision.transform.GetComponent<PlayerMovement>().TomarDaño(dañoAtaque);
        }
    }

   
}

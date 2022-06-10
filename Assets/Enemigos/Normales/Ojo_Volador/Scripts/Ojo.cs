using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ojo : MonoBehaviour
{

    [Header("Rangos")]
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float vida;
    [SerializeField] float daño;

    [Header("Vuelo")]
    [SerializeField] float flyDistance;
    [SerializeField] float flyVelocity;

    [Header("Movimiento")]
    [SerializeField] float velocidadMovimiento;

    [Header("Jugador")]
    [SerializeField] private GameObject player;

    [Header("Sonidos")]
    [SerializeField] private AudioSource reproducir;
 
    private float initPosY;
    private float contador;
    private Rigidbody2D eyeRb;
    private Animator mAnimator;
    private BoxCollider2D ojoCollider;

    private bool isAttacking;
    private bool isDeath;

    // Start is called before the first frame update
    void Start()
    {
        eyeRb = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        ojoCollider = GetComponent<BoxCollider2D>();
        initPosY = transform.position.y;
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
                followPlayer();
                attackPlayer(distPlayer);
            }
            else
            {
                Fly(0, 0, 0);
            }
        }
    }

    private void attackPlayer(float distPlayer)
    {
        if (distPlayer < attackRange)
        {
            if (transform.position.x > player.transform.position.x)
            {
                StartCoroutine("Attack", -10);
            }
            else if (transform.position.x < player.transform.position.x)
            {
                StartCoroutine("Attack", 10);
            }
        }
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
    private IEnumerator Dañado()
    {
        mAnimator.SetTrigger("Hit");
        eyeRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);


    }

    private IEnumerator Morir()
    {
        mAnimator.SetBool("IsDeath", true);
        mAnimator.SetBool("IsFlying", false);
        isDeath = true;
        ojoCollider.enabled = false;
        reproducir.Stop();
        eyeRb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }

    private void followPlayer()
    {
        if (!isAttacking)
        {
            if (transform.position.x > player.transform.position.x)
            {
                eyeRb.velocity = new Vector2(-velocidadMovimiento * Time.deltaTime, 0);
                transform.eulerAngles = new Vector2(0, 0);
                Fly(eyeRb.velocity.x, 0, 1f);
                mAnimator.SetBool("IsFlying", true);
            }
            else if (transform.position.x < player.transform.position.x)
            {
                eyeRb.velocity = new Vector2(velocidadMovimiento * Time.deltaTime, 0);
                transform.eulerAngles = new Vector2(0, 180);
                Fly(eyeRb.velocity.x, 0, 1f);
                mAnimator.SetBool("IsFlying", true);
            }
        }

    }

    private void Fly(float x, float y, float flyPersDistance)
    {
        contador += Time.deltaTime * flyVelocity;
        transform.position = new Vector2(transform.position.x, Mathf.PingPong(contador, flyDistance - flyPersDistance) + initPosY);
        eyeRb.velocity = new Vector2(x, y);
        mAnimator.SetBool("IsFlying", true);
    }
    private IEnumerator Attack(float x)
    {
        isAttacking = true;
        eyeRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        eyeRb.velocity = new Vector2(x, 0);
        mAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        eyeRb.velocity = new Vector2(0, 0);
        isAttacking = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prota"))
        {
            collision.transform.GetComponent<PlayerMovement>().TomarDaño(daño);
        }
    }

}

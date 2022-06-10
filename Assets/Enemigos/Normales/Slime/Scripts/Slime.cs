using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Rangos")]
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float vida;
    [SerializeField] float dañoAtaque;

    [Header("Movimiento")]
    [SerializeField] float velocidadMovimiento;

    [Header("Jugador")]
    [SerializeField] private GameObject player;

    [Header("Sonidos")]
    [SerializeField] private AudioSource reproducir;

    private Rigidbody2D slimeRb;
    private Animator mAnimator;
    private BoxCollider2D slimeCollider;

    private bool isAttacking;
    private bool isDeath;
    // Start is called before the first frame update
    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        mAnimator = gameObject.GetComponent<Animator>();
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
        slimeRb.velocity = new Vector2(0, 0);
        mAnimator.SetBool("IsWalking", false);
        mAnimator.SetBool("IsIdle", true);


    }
    private void FollowPlayer()
    {
        if (!isAttacking)
        {
            if (transform.position.x > player.transform.position.x)
            {
                Walking(-velocidadMovimiento, 0);
            }
            else if (transform.position.x < player.transform.position.x)
            {
                Walking(velocidadMovimiento, 180);
            }
        }

    }
    private void AttackPlayer(float distPlayer)
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

    private void Walking(float x, float rotation)
    {
        slimeRb.velocity = new Vector2(x * Time.deltaTime, 0);
        transform.eulerAngles = new Vector2(0, rotation);
        mAnimator.SetBool("IsWalking", true);
        mAnimator.SetBool("IsIdle", false);

    }

    private IEnumerator Attack(float x)
    {
        isAttacking = true;
        mAnimator.SetBool("IsRunning", false);
        mAnimator.SetBool("IsIdle", false);
        slimeRb.velocity = new Vector2(0, 0);
        mAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        slimeRb.velocity = new Vector2(x, 0);        
        yield return new WaitForSeconds(1);
        isAttacking = false;
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
        mAnimator.SetTrigger("Damaged");
        slimeRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);


    }

    private IEnumerator Morir()
    {
        mAnimator.SetBool("IsDeath", true);
        isDeath = true;
        slimeCollider.enabled = false;
        reproducir.Stop();
        slimeRb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prota"))
        {
            collision.transform.GetComponent<PlayerMovement>().TomarDaño(dañoAtaque);
        }
    }
}

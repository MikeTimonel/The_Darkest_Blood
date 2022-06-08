using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Rangos")]
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float life;

    [Header("Movimiento")]
    [SerializeField] float velocidadMovimiento;

    [Header("Jugador")]
    [SerializeField] private GameObject player;

    private Rigidbody2D slimeRb;
    private Animator mAnimator;

    private bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        mAnimator = gameObject.GetComponent<Animator>();
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
}

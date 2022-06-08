using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ojo : MonoBehaviour
{

    [Header("Rangos")]
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float life;

    [Header("Vuelo")]
    [SerializeField] float flyDistance;
    [SerializeField] float flyVelocity;

    [Header("Movimiento")]
    [SerializeField] float velocidadMovimiento;

    [Header("Jugador")]
    [SerializeField] private GameObject player;

    private float initPosY;
    private float contador;
    private Rigidbody2D eyeRb;

    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        eyeRb = GetComponent<Rigidbody2D>();
        initPosY = transform.position.y;
    }
    
    // Update is called once per frame
    void FixedUpdate()
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

    private void followPlayer()
    {
        if (!isAttacking)
        {
            if (transform.position.x > player.transform.position.x)
            {
                eyeRb.velocity = new Vector2(-velocidadMovimiento * Time.deltaTime, 0);
                transform.eulerAngles = new Vector2(0, 0);
                Fly(eyeRb.velocity.x, 0, 1f);
            }
            else if (transform.position.x < player.transform.position.x)
            {
                eyeRb.velocity = new Vector2(velocidadMovimiento * Time.deltaTime, 0);
                transform.eulerAngles = new Vector2(0, 180);
                Fly(eyeRb.velocity.x, 0, 1f);
            }
        }

    }

    private void Fly(float x, float y, float flyPersDistance)
    {
        contador += Time.deltaTime * flyVelocity;
        transform.position = new Vector2(transform.position.x, Mathf.PingPong(contador, flyDistance - flyPersDistance) + initPosY);
        eyeRb.velocity = new Vector2(x, y);
    }
    private IEnumerator Attack(float x)
    {
        isAttacking = true;
        eyeRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        eyeRb.velocity = new Vector2(x, 0);
        yield return new WaitForSeconds(0.5f);
        eyeRb.velocity = new Vector2(0, 0);
        isAttacking = false;
    }

}

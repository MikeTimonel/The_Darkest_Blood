using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envidia : MonoBehaviour
{
    public Animator Envidiaanimator;
    [SerializeField] private Transform AtaqueEnvidia;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float damageAtaque;
    public GameObject firebomb;
    public PlayerMovement2 playerscript;
    public int Ataque;
    public CircleCollider2D collfire;
    [SerializeField] public int Vida;
    public bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Ataques());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Ataque == 0)
        {
            Envidiaanimator.SetInteger("Ataque", 0);

        }
        else if (Ataque == 1)
        {
            firebomb.SetActive(true);
            Envidiaanimator.SetInteger("Ataque", 1);
            Attacks();
        }
    }
    public void TomarDaño(int daño)
    {
        Vida -= daño;
        if (Vida <= 0)
        {
            playerscript.Protagonista.SetBool("Run", false);
            playerscript.Protagonista.SetBool("Attack", false);
            playerscript.Protagonista.SetBool("Hit", false);
            playerscript.Protagonista.SetBool("Jump", false);
            StartCoroutine(Finalfinal());
        }
    }
    private IEnumerator Ataques()
    {
        while (Vida > 0 && playerscript.life > 0)
        {
            yield return new WaitForSeconds(1.39f);
            Ataque = Random.Range(0, 2);
            yield return new WaitForSeconds(0.3f);
            Ataque = 0;
        }

    }
    private void Attacks()
    {
        StartCoroutine(Esperar());

        Collider2D[] objetos = Physics2D.OverlapCircleAll(AtaqueEnvidia.position, radioAtaque);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Prota"))
            {
                colisionador.transform.GetComponent<PlayerMovement2>().TomarDaño(damageAtaque);
                if (playerscript.life <= 0)
                {
                    playerscript.Protagonista.SetBool("Run", false);
                    playerscript.Protagonista.SetBool("Attack", false);
                    playerscript.Protagonista.SetBool("Jump", false);
                    playerscript.Protagonista.SetBool("Hit", false);
                    playerscript.Protagonista.SetBool("Idle", false);
                    playerscript.Protagonista.SetBool("Fall", false);
                    playerscript.Protagonista.SetBool("Dash", false);
                    playerscript.Protagonista.SetBool("Death", true);
                    playerscript.enabled = false;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AtaqueEnvidia.position, radioAtaque);
    }
    private IEnumerator Esperar()
    {
        yield return new WaitForSeconds(0.67f);
        collfire.enabled = true;
        yield return new WaitForSeconds(0.33f);
        collfire.enabled = false;
        firebomb.SetActive(false);
    }
    private IEnumerator Finalfinal()
    {
        playerscript.speedMovement = 0;
        yield return new WaitForSeconds(0.001f);
        playerscript.Protagonista.SetBool("Idle", true);
        Envidiaanimator.SetInteger("Vida", 0);
        playerscript.enabled = false;
    }
}

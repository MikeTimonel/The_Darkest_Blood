using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avariciascript : MonoBehaviour
{
    public Animator Avaricia;
    [SerializeField] private Transform AtaqueAvaricia;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;
    public PlayerMovement playerscript;
    public int Ataque;
    [SerializeField] public int Vida;
    public bool hit = false;
    void Start()
    {
        StartCoroutine(Ataques());
    }
    void FixedUpdate()
    {
        if (Ataque == 0)
        {
            Avaricia.SetInteger("Ataque", 0);

        }
        else if (Ataque == 1)
        {
            Avaricia.SetInteger("Ataque", 1);
            Attacks();
        }
        else if (Ataque == 2)
        {
            Avaricia.SetInteger("Ataque", 2);
            Attacks();
        }
        else
        {
            Avaricia.SetInteger("Ataque", 3);
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
            Ataque = Random.Range(0, 4);
            yield return new WaitForSeconds(0.3f);
            Ataque = 0;
        }

    }
    private void Attacks()
    {
        StartCoroutine(Esperar());
        Collider2D[] objetos = Physics2D.OverlapCircleAll(AtaqueAvaricia.position, radioAtaque);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Prota"))
            {
                colisionador.transform.GetComponent<PlayerMovement>().TomarDaño(dañoAtaque);
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
        Gizmos.DrawWireSphere(AtaqueAvaricia.position, radioAtaque);
    }
    private IEnumerator Esperar()
    {
        yield return new WaitForSeconds(0.49f);
    }
    private IEnumerator Finalfinal()
    {
        playerscript.speedMovement = 0;
        yield return new WaitForSeconds(0.001f);
        playerscript.Protagonista.SetBool("Idle", true);
        Avaricia.SetInteger("Vida", 0);        
        playerscript.enabled = false;
    }
}

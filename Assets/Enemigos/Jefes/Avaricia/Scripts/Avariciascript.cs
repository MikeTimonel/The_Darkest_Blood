using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avariciascript : MonoBehaviour
{
    public Animator Avaricia;
    public int Ataque;
    [SerializeField] public int Vida;
    public bool hit = false;
    void Start()
    {
        StartCoroutine(Ataques());
    }
    void FixedUpdate()
    {
        if (Ataque ==0)
        {
            Avaricia.SetInteger("Ataque", 0);
           
        }else if (Ataque == 1)
        {
            Avaricia.SetInteger("Ataque", 1);
        }
        else if (Ataque == 2)
        {
            Avaricia.SetInteger("Ataque", 2);
        }
        else
        {
            Avaricia.SetInteger("Ataque", 3);
        }
    }

    public void TomarDaño(int daño)
    {
        Vida -= daño;
        if (Vida <= 0)
        {
            Avaricia.SetInteger("Vida", 0);
        }
        else
        {
            hit = true;
        }

    }

    private IEnumerator Ataques()
    {
        while (Vida > 0)
        {
            if (hit == true)
            {
                yield return new WaitForSeconds(0.11f);
                hit = false;
            }
            else
            {
                yield return new WaitForSeconds(1.05f);
                Ataque = Random.Range(0, 4);
                yield return new WaitForSeconds(0.54f);
                Ataque = 0;
            }
        }
        
    }
}

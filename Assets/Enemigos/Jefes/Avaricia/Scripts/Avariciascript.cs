using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avariciascript : MonoBehaviour
{
    public Animator Avaricia;
    public int Ataque;
    public int Vida;

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
    private IEnumerator Ataques()
    {
        while (Vida > 0)
        {
            yield return new WaitForSeconds(2.1f);
            Ataque = Random.Range(0, 4);
            yield return new WaitForSeconds(0.54f);
            Ataque = 0;
        }
        
    }
}

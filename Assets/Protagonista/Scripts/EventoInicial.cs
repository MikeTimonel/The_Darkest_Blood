using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoInicial: MonoBehaviour
{
    public SpriteRenderer Protareal;
    public GameObject Protacine;
    public GameObject Iniciador;
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "InicioCollider")
        {
            Protareal.enabled = true;
            Destroy(Protacine);
            Destroy(Iniciador);
        }

    }
}

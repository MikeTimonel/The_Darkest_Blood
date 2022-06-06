using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventos : MonoBehaviour
{
    public Animator anima;
    public GameObject Iniciador;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.name == "InicioCollider")
        {
            anima.enabled = false;
            Destroy(Iniciador);
        }
    }
}

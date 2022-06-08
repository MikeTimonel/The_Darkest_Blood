using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventos : MonoBehaviour
{
    public GameObject Protagonista;
    public GameObject Iniciador;
    public GameObject Cinematica;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.name == "InicioCollider")
        {
            Protagonista.SetActive(true);
            Destroy(Iniciador);
            Destroy(Cinematica);
        }
    }
}

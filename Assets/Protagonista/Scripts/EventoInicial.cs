using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class EventoInicial: MonoBehaviour
{

    public SpriteRenderer Protareal;
    public GameObject Protacine;
    public Animator protacinematico;
    public GameObject Iniciador;

    void Awake()
    {
        momento();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "InicioCollider")
        {
            Protareal.enabled = true;
            Destroy(Protacine);
            Destroy(Iniciador);
        }
    }
    public async void momento()
    {
        await Task.Delay(1300);
        protacinematico.enabled = true;
    }
}

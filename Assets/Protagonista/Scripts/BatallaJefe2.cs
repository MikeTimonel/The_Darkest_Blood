using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaJefe2 : MonoBehaviour
{
    public GameObject Activador3;
    //public GameObject Activador2;
    public GameObject Bloqueo2;
    public Camara camarascript;
    public Envidia jefescript;
    void OnCollisionEnter2D(Collision2D coll)
    {
        //if (coll.gameObject.name == "Activador2")
        //{
        //    camarascript.enabled = false;
        //    Destroy(Activador2);
        //}
        if (coll.gameObject.name == "Activador3")
        {
            camarascript.enabled = false;
            jefescript.enabled = true;
            Bloqueo2.SetActive(true);
            Destroy(Activador3);
        }
    }
}

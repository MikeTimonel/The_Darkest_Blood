using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaJefe : MonoBehaviour
{

    public GameObject Activador3;
    public GameObject Activador2;
    public Camara camarascript;
    public Avariciascript jefescript;
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Activador2")
        {
            //camarascript.enabled = false;
            //Destroy(Activador2);
        }
        if (coll.gameObject.name == "Activador3")
        {
            camarascript.enabled = false;
            jefescript.enabled = true;
            Destroy(Activador3);
        }
    }
}

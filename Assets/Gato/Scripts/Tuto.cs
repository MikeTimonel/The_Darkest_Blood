using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public GameObject gatito;

    void FixedUpdate()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "Protagonista")
        {
            gatito.SetActive(true);
        }
    }
}

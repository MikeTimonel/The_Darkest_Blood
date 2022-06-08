using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject jugador;
    public Vector2 minimo;
    public Vector2 maximo;
    public float suavizado;
    Vector2 velocidad;

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, jugador.transform.position.x, ref velocidad.x, suavizado);
        float posY = Mathf.SmoothDamp(transform.position.y, jugador.transform.position.y + 2, ref velocidad.y, suavizado);

        transform.position = new Vector3(Mathf.Clamp(posX, minimo.x, maximo.x), Mathf.Clamp(posY, minimo.y, maximo.y), transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo_Movimiento : MonoBehaviour
{

    [SerializeField] private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugadorRB;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Prota").GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        offset = (jugadorRB.velocity.x/10f) *velocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}

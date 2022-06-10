using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public Animator animadorgato;
    public PlayerMovement protagonistascript;


    void FixedUpdate()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "Numero1")
        {
            animadorgato.SetInteger("Tuto", 1);

        }
        else if (collider.gameObject.name == "Numero2")
        {
            
        }
        else if (collider.gameObject.name == "Numero3")
        {

        }
        else if (collider.gameObject.name == "Numero4")
        {

        }
        else if (collider.gameObject.name == "Numero5")
        {

        }
        else if (collider.gameObject.name == "Numero6")
        {

        }
        else if (collider.gameObject.name == "Numero7")
        {

        }
        else if (collider.gameObject.name == "Numero8")
        {

        }
        else if (collider.gameObject.name == "Numero9")
        {

        }
    }
    private IEnumerator rutina1()
    {
        yield return new WaitForSeconds(5f);
    }
}

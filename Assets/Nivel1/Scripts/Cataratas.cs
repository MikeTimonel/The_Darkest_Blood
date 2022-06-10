using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cataratas : MonoBehaviour
{
    public Cataratas scriptcata;
    public SpriteRenderer catas;
    public GameObject cataratas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D colisionador)
    {
        if(colisionador.gameObject.name == "Protagonista")
        {
            cataratas.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                cataratas.SetActive(false);
                this.catas.enabled = true;
                this.scriptcata.enabled = false;
            }        
        }
    }
}

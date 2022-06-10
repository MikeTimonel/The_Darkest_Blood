using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_bomb : MonoBehaviour
{
    
    public PlayerMovement2 playerscript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "Protagonista")
        {
            playerscript.TomarDaño(21);
        }
    }
}

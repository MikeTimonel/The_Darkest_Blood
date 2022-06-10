using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : MonoBehaviour
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
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Protagonista")
        {
            playerscript.TomarDaño(2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueHongo : MonoBehaviour
{
    private Hongo hongo;
    // Start is called before the first frame update
    void Start()
    {
        hongo = FindObjectOfType<Hongo>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prota"))
        {
            collision.transform.GetComponent<PlayerMovement>().TomarDaño(hongo.dañoAtaque);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private PotionCount potions;
    private void Start()
    {
        potions = FindObjectOfType<PotionCount>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Destroy(this.gameObject);
            potions.potions++;

        }
    }
}

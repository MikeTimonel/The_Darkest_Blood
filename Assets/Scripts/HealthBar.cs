using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthImage;
    private float currentHealth;
    Player mPlayer;
    private float maxHealth;

    void Start()
    {
        healthImage = GetComponent<Image>();
        mPlayer = FindObjectOfType<Player>();
        maxHealth = mPlayer.life;
        
    }
    void Update()
    {
        currentHealth = mPlayer.life;
        healthImage.fillAmount = currentHealth / maxHealth;
    }
}

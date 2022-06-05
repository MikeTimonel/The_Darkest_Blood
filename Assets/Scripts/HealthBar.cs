using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    private Image healthBar;
    private Image damageBar;
    private float damagedHealthBarTimer;
    private Player player;

    [SerializeField] private float currentHealth;
    private float maxHealth;
    private void Start()
    {
        healthBar = transform.Find("HealthBar").GetComponent<Image>();
        damageBar = transform.Find("DamageBar").GetComponent<Image>();
        player = FindObjectOfType<Player>();
        maxHealth = player.life;
    }

    private void Update()
    {
        SetHeath(player.life);
    }

    private void SetHeath(float life) 
    {
        currentHealth = life;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (healthBar.fillAmount < damageBar.fillAmount)
        {
            damageBar.fillAmount -= 0.1f * Time.deltaTime;
        }
        else 
        {
            damageBar.fillAmount = healthBar.fillAmount;
        }
    }
}
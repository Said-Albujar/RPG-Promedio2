using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int damageToPlayer;
    public int experienceToPlayer;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.GainExperience(experienceToPlayer);
        }

        Destroy(gameObject);
    }
}

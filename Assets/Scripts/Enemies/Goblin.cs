using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblin : Enemy
{
    public Slider HealthBarr;
    public float moveSpeed = 3.0f; 

    private Transform player; 

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        maxHealth = 50;
        currentHealth = maxHealth;
        damageToPlayer = 10;
        experienceToPlayer = 25;
    }

    void Update()
    {
        HealthBarr.maxValue = maxHealth;
        HealthBarr.value = currentHealth;

        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.Translate(direction * moveSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance < 1.0f) 
            {
                Player playerController = player.GetComponent<Player>();
                if (playerController != null)
                {
                    playerController.TakeDamage(damageToPlayer);
                }
            }
        }
    }
}
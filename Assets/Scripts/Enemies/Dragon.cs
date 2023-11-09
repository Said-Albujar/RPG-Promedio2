using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : Enemy
{
    public Slider HealthBarr;
    public float moveSpeed = 5.0f;
    public float stoppingDistance; 

    private Transform player; 

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        maxHealth = 100;
        currentHealth = maxHealth;
        damageToPlayer = 15;
        experienceToPlayer = 50;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                Vector3 direction = player.position - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            }
        }

        HealthBarr.maxValue = maxHealth;
        HealthBarr.value = currentHealth;
    }
}

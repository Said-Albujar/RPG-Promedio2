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

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots = 2.0f;

    private Transform player;
    private float shotTimer = 0.0f;

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

                RotateTowardsPlayer();
            }
            else
            {
                shotTimer += Time.deltaTime;

                if (shotTimer >= timeBetweenShots)
                {
                    Shoot();
                    shotTimer = 0.0f;
                }
            }
        }

        HealthBarr.maxValue = maxHealth;
        HealthBarr.value = currentHealth;
    }
    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f); // Puedes ajustar la velocidad de rotación aquí
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        enemyBullet.damage = damageToPlayer; 
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt.API;
using GameJolt.UI;

public class Spawn : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;

    public float baseSpawnInterval = 10.0f;
    public float currentSpawnInterval;
    public float intervalReductionPerLevel = 2.0f;

    void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        if (currentSpawnInterval == 2)
        {
            Trophies.TryUnlock(215937);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(currentSpawnInterval);

        }
    }
    void SpawnEnemy()
    {
        GameObject enemyType1 = Instantiate(enemy1Prefab, transform.position, Quaternion.identity);

        GameObject enemyType2 = Instantiate(enemy2Prefab, transform.position, Quaternion.identity);
    }
}

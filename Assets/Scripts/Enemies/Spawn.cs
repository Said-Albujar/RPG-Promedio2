using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public Player player;

    private float baseSpawnInterval = 10.0f;
    private float currentSpawnInterval;
    private float intervalReductionPerLevel = 2.0f;

    void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject enemyType1 = Instantiate(enemy1Prefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(currentSpawnInterval);

            GameObject enemyType2 = Instantiate(enemy2Prefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(currentSpawnInterval);

            ReduceSpawnInterval();
        }
    }

    void ReduceSpawnInterval()
    {
        int playerLevel = player.playerLevel;

        currentSpawnInterval -= intervalReductionPerLevel * playerLevel;

        if (currentSpawnInterval < 2)
        {
            currentSpawnInterval = 2; 
        }
    }
}

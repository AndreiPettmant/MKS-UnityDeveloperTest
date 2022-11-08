using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn transforms")]
    [SerializeField] private Transform[] spawnPoints;
    [Header("Enemys")]
    [SerializeField] private GameObject[] enemyToSpawn;
    [SerializeField] GameHandler gameHandler;

    private void Start()
    {
        float enemySpawnTime = PlayerPrefs.GetFloat("SpawnTime");

        if(enemySpawnTime == 0)
        {
            enemySpawnTime = 5;
        }
        InvokeRepeating("SpawnEnemy", 0f, enemySpawnTime);
    }

    private void SpawnEnemy()
    {
        if (!gameHandler.isOver)
        {
            int randomEnemy = Random.Range(0, enemyToSpawn.Length);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            GameObject enemy = Instantiate(enemyToSpawn[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }
}

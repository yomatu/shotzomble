using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public Transform[] spawnPoints;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f; 
    public float spawnRadius = 0f; 

    void Start()
    {
        StartCoroutine(SpawnEnemies()); 
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        { 
            //random time 
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime)); 

            // random enemy
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            // random spawnPoint
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            // random position
            Vector3 spawnPosition = spawnPoint.position + Random.insideUnitSphere * spawnRadius; 
            // instantiate enemy
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }
}
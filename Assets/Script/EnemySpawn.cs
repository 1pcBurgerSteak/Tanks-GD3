using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public Transform[] spawnPoints; 

    public int waveCount = 0;
    public int enemiesSpawned = 0; 
    public int spawnCount = 3;
    private float spawnTimer = 0f; 
    private float waveTimer = 0f;
    public bool waveActive = false;

    void Update()
    {
        

        if (!waveActive)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer >= 5f) 
            {
                waveTimer = 0f;
                waveCount++;
                waveActive = true;
                enemiesSpawned = 0;

                if (waveCount % 2 == 0)
                {
                    spawnCount += 2;
                }

                Debug.Log("Wave " + waveCount + " started! Spawn count: " + spawnCount);
            }
        }
        else
        {

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= Random.Range(2f, 5f)) 
            {
                spawnTimer = 0f;

                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                if (enemiesSpawned < spawnCount - 1 || (waveCount % 5 != 0))
                {

                    Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                    Debug.Log("Enemy spawned!");
                }
                else
                {

                    Instantiate(bossPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                    Debug.Log("Boss spawned!");
                }

                enemiesSpawned++;

                if (enemiesSpawned >= spawnCount)
                {
                    waveActive = false;
                    Debug.Log("Wave " + waveCount + " finished!");
                }
            }
        }
    }
}

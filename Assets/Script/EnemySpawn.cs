using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;

    public int waveCount = 0;
    public int spawnCount = 3;
    private float spawnTimer = 0f;
    private float waveTimer = 0f;
    public bool waveActive = false;

    private int enemiesSpawned = 0; // Tracks enemies spawned in the current wave.
    private List<GameObject> activeEnemies = new List<GameObject>(); // Tracks currently active enemies.

    void Update()
    {
        if (!waveActive)
        {
            // Wait for a delay before starting the next wave.
            waveTimer += Time.deltaTime;

            if (waveTimer >= 5f && AllEnemiesDestroyed())
            {
                waveTimer = 0f;
                waveCount++;
                waveActive = true;
                enemiesSpawned = 0;

                // Increase spawn count every 2 waves.
                if (waveCount % 2 == 0)
                {
                    spawnCount += 2;
                }

                Debug.Log("Wave " + waveCount + " started! Spawn count: " + spawnCount);
            }
        }
        else
        {
            // Spawn enemies at intervals.
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= Random.Range(2f, 5f) && enemiesSpawned < spawnCount)
            {
                spawnTimer = 0f;

                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject enemy;

                if (enemiesSpawned < spawnCount - 1 || (waveCount % 5 != 0))
                {
                    enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                    Debug.Log("Enemy spawned!");
                }
                else
                {
                    enemy = Instantiate(bossPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                    Debug.Log("Boss spawned!");
                }

                activeEnemies.Add(enemy); // Add the spawned enemy to the list.
                enemiesSpawned++;
            }

            // End the wave when all enemies are spawned.
            if (enemiesSpawned >= spawnCount)
            {
                waveActive = false;
                Debug.Log("Wave " + waveCount + " finished!");
            }
        }
    }

    // Check if all spawned enemies are destroyed.
    private bool AllEnemiesDestroyed()
    {
        // Remove any null entries (destroyed enemies) from the list.
        activeEnemies.RemoveAll(enemy => enemy == null);

        return activeEnemies.Count == 0;
    }
}

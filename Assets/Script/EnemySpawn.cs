using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Regular enemy prefab
    public GameObject bossPrefab; // Boss enemy prefab
    public Transform[] spawnPoints; // Array of possible spawn points

    public int waveCount = 0; // Tracks the current wave number
    public int enemiesSpawned = 0; // Tracks how many enemies have spawned in the current wave
    public int spawnCount = 3; // Base number of enemies per wave
    private float spawnTimer = 0f; // Timer for spawning enemies
    private float waveTimer = 0f; // Timer for starting the next wave
    public bool waveActive = false; // Flag to check if a wave is active

    void Update()
    {
        // Check if the wave is over and handle starting the next wave
        if (!waveActive)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer >= 5f) // Delay between waves
            {
                waveTimer = 0f;
                waveCount++;
                waveActive = true;
                enemiesSpawned = 0; // Reset the spawn count for the wave

                // Increase spawnCount by 2 every 2 waves
                if (waveCount % 2 == 0)
                {
                    spawnCount += 2;
                }

                Debug.Log("Wave " + waveCount + " started! Spawn count: " + spawnCount);
            }
        }
        else
        {
            // Handle enemy spawning
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= Random.Range(2f, 5f)) // Random spawn interval
            {
                spawnTimer = 0f;

                // Choose a random spawn point from the array
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                if (enemiesSpawned < spawnCount - 1 || (waveCount % 5 != 0))
                {
                    // Spawn regular enemies
                    Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                    Debug.Log("Enemy spawned!");
                }
                else
                {
                    // Spawn boss as the last enemy of every 5th wave
                    Instantiate(bossPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                    Debug.Log("Boss spawned!");
                }

                enemiesSpawned++;

                // Check if the wave is finished
                if (enemiesSpawned >= spawnCount)
                {
                    waveActive = false; // End the wave
                    Debug.Log("Wave " + waveCount + " finished!");
                }
            }
        }
    }
}

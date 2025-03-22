using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public SingleplayerManager manager;

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;

    public int waveCount = 1; // Start with Wave 1
    public int spawnCount = 3; // Initial spawn count
    private float spawnTimer = 0f;
    private float waveTimer = 0f;
    public bool waveActive = false; // Tracks if a wave is currently active

    private int enemiesSpawned = 0; // Tracks the number of enemies spawned in the current wave
    private List<GameObject> activeEnemies = new List<GameObject>(); // Tracks currently active enemies

    void Start()
    {
        manager = FindObjectOfType<SingleplayerManager>();
        NotifyWaveChange(); // Notify manager about the initial wave
    }

    void Update()
    {
        if (!waveActive)
        {
            // Wait before starting the next wave
            waveTimer += Time.deltaTime;

            if (waveTimer >= 1f && AllEnemiesDestroyed())
            {
                StartNextWave();
                Debug.Log("Start");
            }
        }
        else
        {
            // Spawn enemies during the wave
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= Random.Range(2f, 5f) && enemiesSpawned < spawnCount)
            {
                spawnTimer = 0f;
                SpawnEnemy();
            }

            // End the wave when all enemies are destroyed
            if (enemiesSpawned >= spawnCount && AllEnemiesDestroyed())
            {
                EndCurrentWave();
            }
        }
    }

    private void StartNextWave()
    {
        waveTimer = 0f;
        waveActive = true;
        enemiesSpawned = 0;

        // Increase spawn count every 2 waves
        if (waveCount > 1 && waveCount % 2 == 0)
        {
            spawnCount += 2;
        }

        NotifyWaveChange();
        Debug.Log($"Wave {waveCount} started! Spawn count: {spawnCount}");
    }

    private void SpawnEnemy()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy;

        // Boss spawns only on every 5th wave and as the last enemy in the wave
        if (waveCount % 5 == 0 && enemiesSpawned == spawnCount - 1)
        {
            enemy = Instantiate(bossPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
            Debug.Log("Boss spawned!");
        }
        else
        {
            enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
            Debug.Log("Enemy spawned!");
        }

        activeEnemies.Add(enemy); // Track active enemies
        enemiesSpawned++;
    }

    private void EndCurrentWave()
    {
        waveActive = false;
        waveCount++;
        Debug.Log($"Wave {waveCount - 1} finished!");
        Debug.Log("tapos");
    }

    private void NotifyWaveChange()
    {
        if (manager != null)
        {
            manager.Wave(waveCount); // Notify the manager of the current wave
        }
    }

    private bool AllEnemiesDestroyed()
    {
        // Clean up null entries from the list (destroyed enemies)
        activeEnemies.RemoveAll(enemy => enemy != null && !enemy.activeSelf);

        return activeEnemies.Count == 0;
    }
}

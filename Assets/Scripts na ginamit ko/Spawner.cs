using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Manager manager;

    public Transform[] spawnerLocation;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    public List<GameObject> enemies = new List<GameObject>(); // Dynamic enemy list

    public int currentWave = 1;

    public bool spawn = true;
    public float spawnTimer = 0;
    public int spawnCount = 3; // Number of enemies to spawn per wave
    public int spawnCounter = 0;
    bool bossSpawned = false;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    void Update()
    {
        // Spawn enemies if "spawn" is true
        if (spawn && spawnCounter < spawnCount)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= Random.Range(2, 5))
            {
                // Check if it's a boss wave
                if (currentWave % 5 == 0 && currentWave != 1 && bossSpawned)
                {
                    GameObject boss = Instantiate(bossPrefab, spawnerLocation[Random.Range(0, spawnerLocation.Length)].position, Quaternion.identity);
                    enemies.Add(boss);
                    bossSpawned = true;
                }
                else
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawnerLocation[Random.Range(0, spawnerLocation.Length)].position, Quaternion.identity);
                    enemies.Add(enemy);
                }

                spawnCounter++;
                spawnTimer = 0;
                if (spawnCounter == spawnCount)
                {
                    spawn = false;
                    bossSpawned = false;
                }
            }
        }

        

        // Check if all enemies are defeated
        else if (!spawn && AreAllEnemiesDefeated())
        {
            // Prepare for the next wave
            spawn = true;
            manager.UpdateWave();
            currentWave = manager.wave;

            // Adjust spawn settings for the new wave
            spawnCount = 2 + currentWave * 2;
            spawnCounter = 0;
            enemies.Clear(); // Clear the list for the new wave
        }
    }

    bool AreAllEnemiesDefeated()
    {
        enemies.RemoveAll(e => e == null); // Remove destroyed enemies from the list
        return enemies.Count == 0; // Return true if no enemies remain
    }
}

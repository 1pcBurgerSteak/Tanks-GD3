using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBombManager : MonoBehaviour
{
    public GameObject bombPrefab; // Assign bomb prefab in Inspector
    public float spawnInterval = 2f; // Time between spawns
    public Vector3 spawnArea = new Vector3(10f, 0f, 10f); // Define spawn range
    public float spawnHeight = 10f; // Height at which bombs spawn

    void Start()
    {
        InvokeRepeating("SpawnBomb", 1f, spawnInterval);
    }

    void SpawnBomb()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            spawnHeight,
            Random.Range(-spawnArea.z, spawnArea.z)
        );

        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }
}

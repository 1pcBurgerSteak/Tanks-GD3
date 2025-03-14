using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SUMMARY
// At start, a function calls a spawn crate repeatedly with the time interval
// Spawns a prefab crate within the x and z axis
// Y axis must have a high value so the crate spawns from above and fall
// DEV: Gio Salceda

public class CrateManager : MonoBehaviour
{
    [Header("Distance Options")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Crate Options")]
    public GameObject cratePrefab;
    public float spawnInterval;
    public float startingPositionY;

    void Awake()
    {
        // Start spawning crates at regular intervals
        InvokeRepeating(nameof(SpawnCrate), 0f, spawnInterval);
    }

    private void SpawnCrate()
    {
        float randomPositionX = Random.Range(minX, maxX);
        float randomPositionZ = Random.Range(minZ, maxZ);

        Vector3 cratePosition = new Vector3(randomPositionX, startingPositionY, randomPositionZ);

        Instantiate(cratePrefab, cratePosition, cratePrefab.transform.rotation);
    }
}

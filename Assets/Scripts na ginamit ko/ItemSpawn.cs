using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject heal;
    public GameObject life;
    public GameObject gear;
    public float spawnInterval = 1f; // Interval between spawns
    public float lifeSpawnRate = 0.1f; // 10% chance to spawn life

    private float timeSinceLastSpawn;

    public TankShooting tankShooting;
    public float gearTimer = 0f;
    public bool timed = false;

    void Start()
    {
        timeSinceLastSpawn = 0f;
    }

    void Update()
    {
        if (timed)
        {
            //tankShooting.m_ReloadTime = 0.5f;
            gearTimer += Time.deltaTime;
            if (gearTimer > 5f)
            {
                tankShooting.m_ReloadTime = 1f;
                timed = false;
                gearTimer = 0f;
            }
        }

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnItem();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnItem()
    {
        Vector3 randomPosition = GetRandomPosition();

        // Determine which item to spawn
        GameObject itemToSpawn;
        float randomValue = Random.value;
        if (randomValue < lifeSpawnRate)
        {
            itemToSpawn = life;
        }
        else
        {
            itemToSpawn = (Random.value < 0.5f) ? heal : gear;
        }

        GameObject spawnedItem = Instantiate(itemToSpawn, randomPosition, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(spawnedItem.transform.position, 1.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                Destroy(spawnedItem);
                SpawnItem();
                return;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-40f, 40f);
        float z = Random.Range(-40f, 40f);

        return new Vector3(x, 0, z);
    }
}

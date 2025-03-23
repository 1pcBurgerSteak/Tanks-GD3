using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudVariants; // Array to hold multiple cloud variants
    public Vector2 randomSpawnIntervalRange = new Vector2(5f, 20f); // Random spawn interval range
    public Vector2 randomXRange = new Vector2(-35.5f, 120f); // Random X range for spawning
    public Vector2 randomScaleRange = new Vector2(1f, 3f); // Updated random scale range

    float timer = 0f;
    void Start()
    {

    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Random.Range(randomSpawnIntervalRange.x, randomSpawnIntervalRange.y))
        {
            timer = 0f;
            SpawnCloud();
        }
    }

    public void SpawnCloud()
    {
        GameObject cloud = Instantiate(cloudVariants[Random.Range(0, cloudVariants.Length)], new Vector3(Random.Range(randomXRange.x, randomXRange.y), transform.position.y, transform.position.z), Quaternion.identity);
        cloud.transform.localScale = Vector3.one * Random.Range(randomScaleRange.x, randomScaleRange.y);

    }
}
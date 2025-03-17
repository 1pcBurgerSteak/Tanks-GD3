using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkManager : MonoBehaviour
{

    [Header("Position Options")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Shark Options")]
    public GameObject sharkPrefab;
    public float spawnInterval;

    [Header("Target Options")]
    PlayerAddManager playeradd;
    public List<GameObject> players;
    public List<Transform> target1;

    public float time;
 

    public void Start()
    {
        playeradd = GetComponent<PlayerAddManager>();
       
    }
    public void Update()
    {

        time += Time.deltaTime;
        if (time > 10)
        {
            SpawnShark();
            time = 0;
        }

    }
    void Awake()
    {
       
        
    }

    private void SpawnShark()
    {
        float randomPositionX = Random.Range(minX, maxX);
        float randomPositionZ = Random.Range(minZ, maxZ);

        Vector3 cratePosition = new Vector3(randomPositionX, 0, randomPositionZ);

        Instantiate(sharkPrefab, cratePosition, sharkPrefab.transform.rotation);
    }
}

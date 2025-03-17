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
    public GameObject WhirlpoolPrefab;
    public GameObject BombPrefab;
    public float spawnInterval;

    [Header("Target Options")]
    PlayerAddManager playeradd;
    public List<GameObject> players;
    public List<Transform> target1;

    public int rand;
    public float time;
 

    public void Start()
    {
        playeradd = GetComponent<PlayerAddManager>();
        rand = Random.Range(1, 4);
    }
    public void Update()
    {

        time += Time.deltaTime;
        if (time > 5)
        {
            SpawnShark();
           rand = Random.Range(1, 4);
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
        if (rand == 1)
        {
            Instantiate(sharkPrefab, cratePosition, sharkPrefab.transform.rotation);
            Debug.Log("Shark");
        }
        else if (rand == 2)
        {
            Instantiate(WhirlpoolPrefab, cratePosition, sharkPrefab.transform.rotation);
            Debug.Log("Whirlpool");
        }
        else if (rand == 3)
        {
            Instantiate(BombPrefab, cratePosition, sharkPrefab.transform.rotation);
            Debug.Log("Bomb");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

    [Header("Position Options")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Trap Options")]
    public GameObject sharkPrefab;
    public GameObject sharpRock;
    public GameObject BombPrefab;
    public float spawnInterval;

    [Header("Target Options")]
    //PlayerAddManager playeradd;
    public List<GameObject> players;

    public int rand = 0;
    public float time;
 

    public void Start()
    {
        //playeradd = GetComponent<PlayerAddManager>();
    }
    public void Update()
    {
        //Find players
        GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in foundPlayers)
        {
            if (!players.Contains(player))
            {
                players.Add(player);
            }
        }

        //Trap spawner
        time += Time.deltaTime;
        if (time > 5f)
        {
            SpawnTrap();
            rand = Random.Range(1, 4);
            time = 0;
        }

    }

    private void SpawnTrap()
    {
        //Choose random player
        GameObject randomPlayer = players[Random.Range(0, players.Count)];

        float randomPositionX = Random.Range(minX, maxX);
        float randomPositionZ = Random.Range(minZ, maxZ);

        Vector3 trapPosition = new Vector3(randomPositionX, 0, randomPositionZ);
        if (rand == 1)
        {
            GameObject shark = Instantiate(sharkPrefab, trapPosition, sharkPrefab.transform.rotation);
            Shark sharkScript = shark.GetComponent<Shark>();
            sharkScript.target = randomPlayer.transform;
            Debug.Log("Shark");
        }
        else if (rand == 2)
        {
            Instantiate(sharpRock, trapPosition, sharpRock.transform.rotation);
            Debug.Log("Whirlpool");
        }
        else if (rand == 3)
        {
            Instantiate(BombPrefab, trapPosition, BombPrefab.transform.rotation);
            Debug.Log("Bomb");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shark : MonoBehaviour
{
    public NavMeshAgent Enemy;
    private PlayerAddManager addManager;

    public List<Transform> players = new List<Transform>();

    private void Start()
    {
        addManager = FindObjectOfType<PlayerAddManager>();

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        if (playerObjects.Length == 2)
        {
            players.Add(playerObjects[0].transform);
            players.Add(playerObjects[1].transform);
        }
       
    }

    public void Update()
    {
        

        Vector3 near = gameObject.transform.position - players[1].position;
        Vector3 nearest = gameObject.transform.position - players[0].position;

        if (near.magnitude > nearest.magnitude)
        {
            Enemy.SetDestination(players[0].position);
        }
        else
        {
            Enemy.SetDestination(players[1].position);
        }
    }
}

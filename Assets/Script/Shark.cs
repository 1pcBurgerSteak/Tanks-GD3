using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shark : MonoBehaviour
{
    public Transform player;
    public Transform player1;
    public NavMeshAgent Enemy;
    private PlayerAddManager addManager;

   



    private void Start()
    {
        addManager = GetComponent<PlayerAddManager>();
        addManager = FindObjectOfType<PlayerAddManager>();
    }
    public void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        player1 = GameObject.FindWithTag("Player").transform;

        Vector3 nearest = gameObject.transform.position - player1.transform.position;
        Vector3 nearest1 = gameObject.transform.position - player.transform.position;

        if (nearest.x > nearest1.x)
        {
            Enemy.SetDestination(player.position);
        }else if (nearest1.x > nearest.x)
        {
            Enemy.SetDestination(player1.position);
        }

        
    }
}

using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public string playerTag = "Player";
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        // Find the nearest player
        Transform nearestPlayer = FindNearestPlayer();
        if (nearestPlayer != null)
        {
            navMeshAgent.destination = nearestPlayer.position;
        }

        Debug.DrawLine(transform.position, navMeshAgent.destination, Color.red);
    }

    Transform FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = player.transform;
            }
        }
        return nearest;
    }
}

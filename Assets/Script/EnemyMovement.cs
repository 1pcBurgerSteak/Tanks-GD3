using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    private NavMeshAgent navMeshAgent;
    public float stopDistance = 5f; // Distance at which the enemy stops moving

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(player = null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > stopDistance)
            {
                // Move towards the player
                navMeshAgent.SetDestination(player.position);
                navMeshAgent.isStopped = false; // Ensure movement is enabled
            }
            else
            {
                // Stop moving but continue rotating to face the player
                navMeshAgent.isStopped = true;

                // Rotate to face the player
                Vector3 direction = player.position - transform.position;
                direction.y = 0; // Prevent vertical rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5);
            }
        }
    }
}

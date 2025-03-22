using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public string playerTag = "Player";
    private NavMeshAgent navMeshAgent;

    public bool isBoss = false;

    //AudioManager audioManager;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //audioManager = FindObjectOfType<AudioManager>();
        //audioManager.PlaySFX("EnemyMove");
    }

    void Update()
    {

        Transform nearestPlayer = FindNearestPlayer();
        if (nearestPlayer != null)
        {
            // Set the destination for the NavMeshAgent
            navMeshAgent.destination = nearestPlayer.position;

            Vector3 direction = (nearestPlayer.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Ignore the y-axis
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation
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

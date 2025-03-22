using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shark : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;

    private void Start()
    {
        Invoke("DestroyShark", 10f);
    }

    void Update()
    {
        if(target != null)
        {
            navMeshAgent.destination = target.position;
        }
        else
        {
            DestroyShark();
        }
    }

    private void DestroyShark()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIHealth health = other.GetComponent<UIHealth>();
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if(health != null)
            {
                health.TakeDamage(20f);

                Vector3 pushDirection = other.transform.position - transform.position;
                pushDirection.Normalize();

                float pushForce = 10f;
                rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}

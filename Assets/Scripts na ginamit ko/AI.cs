using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject target;
    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float stopDistance = 7f;
    
    private Rigidbody rb;

    EnemyShooting enemyShooting;
    public Detector detector;

    float shootingTime = 0f;
    void Start()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        target = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (detector.inRange)
        {
            shootingTime += Time.deltaTime;
            if(shootingTime >= Random.Range(2, 8))
            {
                enemyShooting.Fire();
                shootingTime = 0;
            }
        }
        else
        {
            shootingTime = 0;
        }
    }

    void FixedUpdate()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget > stopDistance)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        if(distanceToTarget < stopDistance)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}

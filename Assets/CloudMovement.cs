using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    float speed = 0f;
    public float destroyZThreshold = -250;

    void Start()
    {
        speed = Random.Range(5f, 15f);
    }

    void Update()
    {
        // Move the cloud along the Z-axis
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);

        // Destroy the cloud if it reaches the threshold
        if (transform.position.z <= destroyZThreshold)
        {
            Destroy(gameObject);
        }
    }
}

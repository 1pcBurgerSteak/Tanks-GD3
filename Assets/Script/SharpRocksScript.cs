using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpRocksScript : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float moveRange = 3f; 
    public float startPositionY = -9.94f; 

    private Vector3 startPosition;
    private float targetY;
    private bool movingUp = true;

    void Start()
    {
        startPosition = new Vector3(transform.position.x, startPositionY, transform.position.z);
        transform.position = startPosition;
        targetY = startPosition.y + moveRange; 
    }

    void Update()
    {
        if (movingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            if (transform.position.y >= targetY)
            {
                movingUp = false; 
                targetY = startPosition.y - moveRange; 
            }
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

            if (transform.position.y <= targetY)
            {
                movingUp = true; 
                targetY = startPosition.y + moveRange;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIHealth health = other.GetComponent<UIHealth>();
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (health != null)
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

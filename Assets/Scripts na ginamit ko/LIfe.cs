using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIfe : MonoBehaviour
{
    Manager manager;
    void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("wow2");
            manager.life += 1;
            manager.lifeText.text = $"x: {manager.life}";
            Destroy(gameObject);
        }
    }
}

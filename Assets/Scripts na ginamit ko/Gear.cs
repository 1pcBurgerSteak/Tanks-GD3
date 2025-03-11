using Complete;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    float timer = 0f;
    bool timed = false;

    ItemSpawn itemSpawn;
    void Start()
    {
        itemSpawn = FindObjectOfType<ItemSpawn>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            itemSpawn.timed = true;
            TankShooting tankShooting = other.GetComponent<TankShooting>();

            tankShooting.m_ReloadTime = 0.1f;
            Destroy(gameObject);
        }
    }
}

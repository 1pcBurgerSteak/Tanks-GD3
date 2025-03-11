using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("wow");
            TankHealth tankHealth = other.GetComponent<TankHealth>();

            if (tankHealth != null)
            {
                tankHealth.m_CurrentHealth += 50;
                if (tankHealth.m_CurrentHealth > tankHealth.m_StartingHealth)
                {
                    tankHealth.m_CurrentHealth = tankHealth.m_StartingHealth;
                }

                tankHealth.SetHealthUI();

                Destroy(gameObject);
            }
        }
    }
}

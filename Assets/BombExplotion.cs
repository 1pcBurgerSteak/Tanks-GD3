using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplotion : MonoBehaviour
{
    public float explosionDelay = 3f; // Time before explosion
    public float explosionRadius = 5f; // AOE damage radius
    public float explosionForce = 700f; // Explosion impact force
    public GameObject explosionEffect; // Assign explosion VFX prefab
    public int damage = 50; // Damage amount to player

    AudioManager audioManager;

    private bool hasExploded = false;

    void Start()
    {
        Invoke("Explode", explosionDelay);
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Instantiate explosion effect
        if (explosionEffect)
        {
           Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Apply force to nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Deal damage to player
            UIHealth player = nearby.GetComponent<UIHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        audioManager.PlaySFX("ShipDeath");
        // Destroy the bomb object
        Destroy(gameObject);
    }

}

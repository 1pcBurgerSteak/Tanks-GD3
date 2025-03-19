using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Rigidbody enemyShell; // Prefab of the enemy's projectile.
    public Transform firePoint; // The point where the projectile is fired.
    public float minLaunchForce = 10f; // Minimum force applied to the projectile.
    public float maxLaunchForce = 20f; // Maximum force applied to the projectile.
    public float fireRate = 2f; // Time interval between shots.

    public Collider rangeCollider; // Collider for the range detection.
    private Transform targetPlayer; // Keeps track of the nearest player.
    private bool isPlayerInRange = false; // Flag to check if the player is within range.
    private bool canFire = true; // Controls the cooldown between shots.

    public bool isBoss = true;
    void Start()
    {
        // Assign the range collider (ensure the GameObject has a Collider).
        //rangeCollider = GetComponent<Collider>();
    }

    void Update()
    {
        // Fire when the player is within range and the cooldown allows.
        if (isPlayerInRange && canFire && targetPlayer != null)
        {
            Fire(); // Fire at the player.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a player.
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Player is now in range.
            targetPlayer = other.transform; // Track the player's position.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to a player.
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Player is no longer in range.
            targetPlayer = null; // Clear the player's position tracking.
        }
    }

    private void Fire()
    {
        // Prevent firing until cooldown ends.
        canFire = false;

        // Generate a random launch force within the range.
        float randomLaunchForce = Random.Range(minLaunchForce, maxLaunchForce);

        // Instantiate the shell at the fire point.
        Rigidbody shellInstance = Instantiate(enemyShell, firePoint.position, firePoint.rotation);

        // Apply forward velocity to the shell using the fire point's current forward direction.
        shellInstance.velocity = firePoint.forward * randomLaunchForce;

        // Debug line to visualize the shot direction.
        Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * 10f, Color.red, 1f);

        // Start cooldown before the next shot.
        StartCoroutine(FireCooldown());
    }

    private IEnumerator FireCooldown()
    {
        // Cooldown period for the next shot.
        yield return new WaitForSeconds(fireRate);
        canFire = true; // Allow firing again.
    }
}

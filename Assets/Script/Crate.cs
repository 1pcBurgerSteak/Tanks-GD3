using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SUMMARY
// Generate a random number
// That number is an ID for the power up
// When a player collide with this object, it triggers the power up
// DEV: Gio Salceda

public class Crate : MonoBehaviour
{
    private int randomCrateID;

    void Awake()
    {
        randomCrateID = Random.Range(1, 4);
        Debug.Log(randomCrateID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;

            HandlePowerUp(player);
        }
    }

    private void HandlePowerUp(GameObject player)
    {
        if (randomCrateID == 1) // Movement speed
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

            playerMovement.m_Speed += 8;
        }

        if (randomCrateID == 2) // Scale up
        {
            player.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }

        if (randomCrateID == 3) // Multi bullets
        {
            PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();

            //playerShooting.canMultiShoot = true;
        }

        if (randomCrateID == 4) //Rapid Fire
        {

        }

        Destroy(gameObject);
    }
}

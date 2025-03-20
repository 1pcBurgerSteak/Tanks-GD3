using UnityEngine;

public class Crate : MonoBehaviour
{
    public int randomCrateID;
    private GameObject scaledPlayer;

    void Awake()
    {
        // Randomly generate an ID for the power-up.
        //randomCrateID = Random.Range(1, 6); // Updated range to include Spray Fire
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
        PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        switch (randomCrateID)
        {
            case 1: // Movement speed
                
                if (playerMovement != null)
                {
                    playerMovement.EnableSpeedBuff();
                }
                break;

            case 2: // Scale up
                if(playerMovement != null)
                {
                    playerMovement.EnableScaleBuff();
                }
                break;

            case 3: // Scatter Shell
                if (playerShooting != null)
                {
                    playerShooting.EnableScatterShell();
                }
                break;

            case 4: // Triple Shell
                if (playerShooting != null)
                {
                    playerShooting.EnableTripleShell();
                }
                break;

            case 5: // Giant Shell
                if (playerShooting != null)
                {
                    playerShooting.EnableGiantShell();
                }
                break;

            case 6: // Rapid Fire
                if (playerShooting != null)
                {
                    //playerShooting.EnableRapidFire();
                }
                break;

            case 7: // Spray Fire
                if (playerShooting != null)
                {
                    //playerShooting.EnableSprayFire();
                }
                break;
        }

        Destroy(gameObject);
    }
}

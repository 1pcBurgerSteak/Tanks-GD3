using UnityEngine;

public class Crate : MonoBehaviour
{
    private int randomCrateID;

    void Awake()
    {
        // Randomly generate an ID for the power-up.
        randomCrateID = Random.Range(1, 7);
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

        switch (randomCrateID)
        {
            case 1: // Movement speed
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.m_Speed += 8;
                break;
            case 2: // Scale up
                player.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                break;
            case 3: // Scatter Shell
                playerShooting.EnableScatterShell();
                break;
            case 4: // Triple Shell
                playerShooting.EnableTripleShell();
                break;
            case 5: // Giant Shell
                playerShooting.EnableGiantShell();
                break;
            case 6: // Rapid Fire
                playerShooting.EnableRapidFire();
                break;
        }

        Destroy(gameObject); // Destroy the crate after applying the power-up.
    }
}

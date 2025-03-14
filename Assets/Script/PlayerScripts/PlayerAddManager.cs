using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAddManager : MonoBehaviour
{
    public List<InputAction> inputActions; // List of InputActions for inputs
    public GameObject playerPrefab; // Prefab for the player arrow
    private GameObject instantiatedPlayer; // Reference to the instantiated arrow
    private GameObject playerManager;
    public CameraControl cameraControl;

    private void OnEnable()
    {
        // Enable all InputActions
        foreach (var action in inputActions)
        {
            action.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var action in inputActions)
        {
            action.Disable();
        }
    }

    private void Update()
    {
        if(playerManager == null) 
        {
            playerManager = GameObject.Find("PlayerManager");
        }
        HandleInput();
    }

    private void HandleInput()
    {
        if (instantiatedPlayer != null) return;
        foreach (var action in inputActions)
        {
            if (action.triggered)
            {
                InstantiatePlayer();
                break;
            }
        }
    }

    private void InstantiatePlayer()
    {
        if (playerPrefab != null)
        {
            instantiatedPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity, playerManager.transform);


        }
        else
        {
            Debug.LogWarning("Player arrow prefab is not assigned.");
        }
    }
}
 
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Complete
{
    public class PlayerController : MonoBehaviour
    {
        public string controlType;
        private GameObject playerManager;
        private PlayerMovement playerMovement;
        private PlayerShooting playerShooting;
        private GameObject playerObject;
        public GameObject playerPrefab;
        void Start()
        {
            playerManager = GameObject.Find("PlayerManager");
            if (playerManager != null)
            {
                transform.SetParent(playerManager.transform);
            }
            else
            {
                Debug.LogWarning("PlayerManager is not assigned in PlayerController.");
            }
            playerObject = Instantiate(playerPrefab, transform.position,Quaternion.identity);
        }
        void Update()
        {
            if(playerObject != null)
            {
                playerMovement = playerObject.GetComponent<PlayerMovement>();
                playerShooting = playerObject.GetComponent<PlayerShooting>();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 inputValue = context.ReadValue<Vector2>();
            Debug.Log($"{controlType}{inputValue}");
            if(playerMovement != null)
            {
                playerMovement.UpdateMovement(inputValue);
            }
        }
        public void OnShoot(InputAction.CallbackContext context) 
        {
            Debug.Log($"{controlType}: Shoot");
            if(playerShooting != null)
            {
                if(context.performed)
                {
                    playerShooting.Shoot();
                }
                if(context.canceled)
                {
                    playerShooting.UnShoot();
                }
            }
        }
    }
}

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
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 inputValue = context.ReadValue<Vector2>();
            Debug.Log($"{controlType}{inputValue}");
        }
        public void OnShoot(InputAction.CallbackContext context) 
        {
            Debug.Log($"{controlType}: Shoot");
        }
    }
}

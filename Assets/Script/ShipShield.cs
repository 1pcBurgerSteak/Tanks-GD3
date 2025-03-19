using UnityEngine;
using System.Collections;

namespace Complete
{
    public class ShipShield : MonoBehaviour
    {
        public float shieldDuration = 3f; // Shield active time
        public float shieldCooldown = 15f; // Cooldown duration
        public GameObject shieldVisual; // Shield effect
        //public Collider shieldCollider;

        public bool isShieldActive = false;
        public bool isOnCooldown = false;

        private void Start()
        {

        }

        public IEnumerator ActivateShield()
        {
            // Prevent activation if shield is already active or cooling down
            if (isShieldActive || isOnCooldown) yield break;

            isShieldActive = true;
            isOnCooldown = true;

            if (shieldVisual != null)
            {
                shieldVisual.SetActive(true);
                Debug.Log("activate shield");
            }
                //shieldVisual.SetActive(true);

            //if (shieldCollider != null)
              //  shieldCollider.enabled = true;

            yield return new WaitForSeconds(shieldDuration);

            if (shieldVisual != null)
                shieldVisual.SetActive(false);

            //if (shieldCollider != null)
              //  shieldCollider.enabled = false;

            isShieldActive = false;

            yield return new WaitForSeconds(shieldCooldown - shieldDuration);

            isOnCooldown = false;
        }

        public bool IsShieldActive()
        {
            return isShieldActive;
        }

        public bool IsOnCooldown()
        {
            return isOnCooldown;
        }
    }
}

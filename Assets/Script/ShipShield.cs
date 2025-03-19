using UnityEngine;
using System.Collections;

namespace Complete
{
    public class TankShield : MonoBehaviour
    {
        public int maxShields = 3; 
        public float shieldDuration = 15f; 
        public GameObject shieldVisual; // Shield effect 
        public Collider shieldCollider;  
        private TankShooting tankShooting; 

        private int currentShields;
        private bool isShieldActive = false;

        private void Start()
        {
            currentShields = maxShields;
            if (shieldVisual != null)
                shieldVisual.SetActive(false);

            tankShooting = GetComponent<TankShooting>(); // Get shooting component
        }


        public IEnumerator ActivateShield()
        {
            if (isShieldActive || currentShields <= 0) yield break;

            isShieldActive = true;
            if (shieldVisual != null)
                shieldVisual.SetActive(true);

            if (shieldCollider != null)
                shieldCollider.enabled = true; // Enable shield collision

            currentShields--;

            yield return new WaitForSeconds(shieldDuration);

            if (shieldVisual != null)
                shieldVisual.SetActive(false);

            if (shieldCollider != null)
                shieldCollider.enabled = false; // Disable shield collision

            isShieldActive = false;
        }

        public bool IsShieldActive()
        {
            return isShieldActive;
        }
    }
}

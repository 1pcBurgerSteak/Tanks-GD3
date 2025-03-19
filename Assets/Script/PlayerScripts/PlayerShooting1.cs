using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class PlayerShooting1 : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                  // Prefab of the shell.
        public Transform m_FireTransform;          // A child of the tank where the shells are spawned.
        public Slider m_AimSlider;                 // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;        // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;           // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;               // Audio that plays when each shot is fired.
        public float m_MinLaunchForce = 15f;       // The force given to the shell if the fire button is not held.
        public float m_MaxLaunchForce = 30f;       // The force given to the shell if the fire button is held for the max charge time.
        public float m_MaxChargeTime = 0.75f;      // How long the shell can charge for before it is fired at max force.
        public float reloadTime = 2f;              // Time it takes to reload after firing.

        private float m_CurrentLaunchForce;        // The force that will be given to the shell when the fire button is released.
        private float m_ChargeSpeed;               // How fast the launch force increases, based on the max charge time.
        private float reloadTimer;                 // Timer to handle reloading logic.
        private bool m_Fired;                      // Whether or not the shell has been launched with this button press.

        private void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI.
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_MinLaunchForce;

            // Reset the reload timer.
            reloadTimer = 0f;
        }

        private void Start()
        {
            // Calculate the charge speed based on the max charge time.
            m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        }

        private void Update()
        {
            // Update the reload timer.
            if (reloadTimer > 0f)
            {
                reloadTimer -= Time.deltaTime;
            }

            // Update the slider to display the current launch force.
            m_AimSlider.value = m_CurrentLaunchForce;
        }

        public void Shoot()
        {
            // Ensure the shot can only be charged when not reloading.
            if (reloadTimer <= 0f)
            {
                m_Fired = false; // Reset the fired status.
                m_CurrentLaunchForce = m_MinLaunchForce; // Reset the launch force.

                // Play the charging audio clip.
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }
        }

        public void UnShoot()
        {
            // Fire the shell only if it hasn't been launched yet.
            if (!m_Fired && reloadTimer <= 0f)
            {
                Fire();
            }
        }

        private void Fire()
        {
            // Set the fired flag to true.
            m_Fired = true;

            // Instantiate the shell and get a reference to its Rigidbody.
            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity based on the launch force.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

            // Play the firing audio clip.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            // Reset the launch force for the next shot.
            m_CurrentLaunchForce = m_MinLaunchForce;

            // Start the reload timer.
            reloadTimer = reloadTime;
        }
    }
}

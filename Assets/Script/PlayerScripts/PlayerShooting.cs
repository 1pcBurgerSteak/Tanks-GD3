using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Complete;

public class PlayerShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public Transform m_ShieldTransform;
    public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.

    private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.

    public ShipShield shield;

    // Power-Up Modifiers
    [Header("Cannon Ball Modifier")]
    public Rigidbody m_GiantShell;
    public bool Shell_GiantShell = false;
    private Rigidbody m_CurrentShell;           // cannon ball that is currently being used 

    [Header("Player Buffs")]
    public bool Boost_ScatterShell = false;
    public bool Boost_TripleShell = false;
    public bool Boost_RangedShell = false;

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }

    private void Start()
    {
        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        m_Fired = true;
        shield = GetComponent<ShipShield>();
    }

    private void Update()
    {
        // Shell that will be used by the player
        if (Shell_GiantShell) 
        {
            m_CurrentShell = m_GiantShell;
        }
        else
        {
            m_CurrentShell = m_Shell;
        }

        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;

            if (Boost_TripleShell)
            {
                StartCoroutine(TripleBulletShoot());
            }
            else if (Boost_ScatterShell)
            {
                ScatterFire();
            }
            else if (Boost_RangedShell)
            {
                SpeedFire();
            }
            else
                Fire();
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (!m_Fired)
        {
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
            m_AimSlider.value = m_CurrentLaunchForce;
        }
    }

    public void Shoot()
    {
        m_Fired = false;
        m_CurrentLaunchForce = m_MinLaunchForce;
    }

    public void UnShoot()
    {
        if (!m_Fired)
        {
            if (Boost_TripleShell)
            {
                StartCoroutine(TripleBulletShoot());
                return;
            }
            else if (Boost_ScatterShell)
            {
                ScatterFire();
            }
            else
                Fire();
        }
    }

    private void Fire()
    {

        // Set the fired flag so only Fire is only called once.
        m_Fired = true;
        Rigidbody shellInstance;
        // Create an instance of the shell and store a reference to its rigidbody.
        if(shield.isShieldActive)
        {
            shellInstance = Instantiate(m_CurrentShell, m_ShieldTransform.position, m_FireTransform.rotation) as Rigidbody;
        }
        else
        {
            shellInstance = Instantiate(m_CurrentShell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        }
        

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        // Reset the launch force. This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;
    }

    public IEnumerator TripleBulletShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void ScatterFire()
    {
        // Set the fired flag so Fire is only called once.
        m_Fired = true;

        float spreadAngle = 20f;   // Angle variation for left and right bullets.
        float spreadDistance = 0.5f; // Horizontal spacing between bullets.

        // Fire three bullets at different angles and positions.
        FireWithAngle(0, Vector3.zero);                         // Center bullet.
        FireWithAngle(-spreadAngle, -m_FireTransform.right * spreadDistance); // Left bullet.
        FireWithAngle(spreadAngle, m_FireTransform.right * spreadDistance);   // Right bullet.

        // Reset the launch force.
        m_CurrentLaunchForce = m_MinLaunchForce;
    }
    void SpeedFire()
    {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;
        Rigidbody shellInstance;
        // Create an instance of the shell and store a reference to its rigidbody.
        if (shield.isShieldActive)
        {
            shellInstance = Instantiate(m_CurrentShell, m_ShieldTransform.position, m_FireTransform.rotation) as Rigidbody;
        }
        else
        {
            shellInstance = Instantiate(m_CurrentShell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        }

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_MaxLaunchForce * m_FireTransform.forward;

        // Reset the launch force. This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;
    }

    // Function to fire bullets with an angle offset and position offset.
    private void FireWithAngle(float angleOffset, Vector3 positionOffset)
    {
        // Calculate new rotation with an angle offset.
        Quaternion bulletRotation = m_FireTransform.rotation * Quaternion.Euler(0, angleOffset, 0);

        // Instantiate bullet with modified position and rotation.
        Rigidbody shellInstance = Instantiate(m_CurrentShell, m_FireTransform.position + positionOffset, bulletRotation);

        // Apply velocity to the bullet.
        shellInstance.velocity = bulletRotation * (Vector3.forward * m_CurrentLaunchForce);
    }

    // Function to enable the GiantShell power-up.
    public void EnableGiantShell()
    {
        Shell_GiantShell = true;
        Boost_ScatterShell = false;
        Boost_TripleShell = false;
    }

    // Function to enable the TripleShell power-up.
    public void EnableTripleShell()
    {
        Shell_GiantShell = false;
        Boost_ScatterShell = false;
        Boost_TripleShell = true;
    }

    // Function to enable the ScatterShell power-up.
    public void EnableScatterShell()
    {
        Shell_GiantShell = false;
        Boost_ScatterShell = true;
        Boost_TripleShell = false;
    }
    public void EnableRangedShell()
    {
        Shell_GiantShell = false;
        Boost_ScatterShell = false;
        Boost_TripleShell = false;
        Boost_RangedShell = true;
    }
}

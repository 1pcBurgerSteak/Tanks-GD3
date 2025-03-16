using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.

    private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.

    // Power-Up Modifiers
    [Header("Cannon Ball Modifier")]
    public Rigidbody m_GiantShell;
    public bool Shell_GiantShell = false;
    private Rigidbody m_CurrentShell;           // Cannon ball that is currently being used.

    [Header("Player Buffs")]
    public bool Boost_ScatterShell = false;
    public bool Boost_TripleShell = false;
    public bool isRapidFire = false;            // Determines if Rapid Fire mode is active.
    public bool Boost_SprayFire = false;

    private float reloadTimer = 0f;             // Tracks reload time.
    private float reloadTime = 1f;              // Default reload time.

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
    }

    private void Update()
    {
        // Shell that will be used by the player.
        if (Shell_GiantShell)
        {
            m_CurrentShell = m_GiantShell;
        }
        else
        {
            m_CurrentShell = m_Shell;
        }

        // Handle reload timer.
        if (reloadTimer > 0f)
        {
            reloadTimer -= Time.deltaTime;
        }

        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired && reloadTimer <= 0f)
        {
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;

            if (Boost_TripleShell)
            {
                TripleBulletShoot();
            }
            else if (Boost_ScatterShell)
            {
                ScatterFire();
            }
            else
            {
                Fire();
            }
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
        if (reloadTimer <= 0f)
        {
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;
        }
    }

    public void UnShoot()
    {
        if (!m_Fired && reloadTimer <= 0f)
        {
            if (Boost_TripleShell)
            {
                TripleBulletShoot();
                return;
            }
            else if (Boost_ScatterShell)
            {
                ScatterFire();
            }
            else
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        // Prevent firing if reloading.
        if (reloadTimer > 0f) return;

        // Set the fired flag so Fire is only called once.
        m_Fired = true;

        // Set the reload timer depending on Rapid Fire status.
        reloadTimer = isRapidFire ? 0f : reloadTime;

        // Create an instance of the shell and store a reference to its rigidbody.
        Rigidbody shellInstance = Instantiate(m_CurrentShell, m_FireTransform.position, m_FireTransform.rotation);

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        // Reset the launch force. This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;
    }

    private void TripleBulletShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            Fire();
        }
    }

    private void ScatterFire()
    {
        float spreadAngle = 20f;   // Angle variation for left and right bullets.
        float spreadDistance = 0.5f; // Horizontal spacing between bullets.

        // Fire three bullets at different angles and positions.
        FireWithAngle(0, Vector3.zero);                         // Center bullet.
        FireWithAngle(-spreadAngle, -m_FireTransform.right * spreadDistance); // Left bullet.
        FireWithAngle(spreadAngle, m_FireTransform.right * spreadDistance);   // Right bullet.
    }

    private void FireWithAngle(float angle, Vector3 additionalVelocity)
    {
        Rigidbody shellInstance = Instantiate(m_CurrentShell, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.transform.Rotate(0, angle, 0);
        shellInstance.velocity = m_FireTransform.forward * m_CurrentLaunchForce + additionalVelocity;
    }

    public void EnableGiantShell()
    {
        Shell_GiantShell = true;
        Boost_ScatterShell = false;
        Boost_TripleShell = false;
        Invoke("DisableGiantShell", 5f); // Disable GiantShell after 5 seconds.
    }

    public void DisableGiantShell()
    {
        Shell_GiantShell = false;
    }

    public void EnableTripleShell()
    {
        Shell_GiantShell = false;
        Boost_ScatterShell = false;
        Boost_TripleShell = true;
        Invoke("DisableTripleShell", 5f); // Disable TripleShell after 5 seconds.
    }

    public void DisableTripleShell()
    {
        Boost_TripleShell = false;
    }

    public void EnableScatterShell()
    {
        Shell_GiantShell = false;
        Boost_ScatterShell = true;
        Boost_TripleShell = false;
        Invoke("DisableScatterShell", 5f); // Disable ScatterShell after 5 seconds.
    }

    public void DisableScatterShell()
    {
        Boost_ScatterShell = false;
    }

    public void EnableRapidFire()
    {
        isRapidFire = true;
        Invoke("DisableRapidFire", 5f); // Disable Rapid Fire after 5 seconds.
    }

    public void DisableRapidFire()
    {
        isRapidFire = false;
    }

    private void SprayFire()
    {
        float sprayAngle = 45f;   // Spread range.
        int numBullets = 5;       // Number of bullets in the spray.

        Debug.Log("Spray Fire activated!"); // Debug log for spray fire

        for (int i = 0; i < numBullets; i++)
        {
            float angle = -sprayAngle / 2 + (sprayAngle / (numBullets - 1)) * i;
            FireWithAngle(angle, Vector3.zero);
        }
    }


    public void EnableSprayFire() //idk how to simulate flamethrower so i improvised.
    {
        Shell_GiantShell = false;
        Boost_ScatterShell = false;
        Boost_TripleShell = false;
        Boost_SprayFire = true;
        Invoke("DisableSprayFire", 5f);
    }

    public void DisableSprayFire()
    {
        Boost_SprayFire = false;
    }
}

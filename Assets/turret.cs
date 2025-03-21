using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour
{
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_ShootingAudio;
    public AudioClip m_FireClip;
    public float m_MinLaunchForce = 15f;
    public float m_MaxLaunchForce = 30f;
    public float m_MinFireRate = 1f;
    public float m_MaxFireRate = 3f;
    public float m_AimAngleRange = 60f;
    public float m_RotationSpeed = 60f; // Increased rotation speed

    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(m_MinFireRate, m_MaxFireRate));

            // Pick a new random rotation
            float targetAngle = Random.Range(-m_AimAngleRange, m_AimAngleRange);
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f) * transform.rotation;

            yield return StartCoroutine(RotateTurret(targetRotation));

            Debug.Log("Firing!"); // Debug to check if Fire() is called
            Fire();
        }
    }

    private IEnumerator RotateTurret(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 2f) // Increased tolerance
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void Fire()
    {
        

        float launchForce = Random.Range(m_MinLaunchForce, m_MaxLaunchForce);
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.velocity = launchForce * m_FireTransform.forward;
        return;
        
    }
}

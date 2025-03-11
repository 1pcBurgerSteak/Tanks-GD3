using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShooting : MonoBehaviour
{
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public Slider m_AimSlider;
    public AudioSource m_ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;
    public float m_MinLaunchForce = 15f;
    public float m_MaxLaunchForce = 30f;
    public float m_MaxChargeTime = 0.75f;

    public Transform playerTransform;
    private float m_CurrentLaunchForce;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable()
    {
        m_AimSlider.value = m_MinLaunchForce;
    }

    public void Fire()
    {
        if (gameObject.transform.localScale.x == 2 || gameObject.transform.localScale.y == 2 || gameObject.transform.localScale.z == 2)
        {
            Debug.Log("hayop ka");
            AimAtPlayer();
        }
            
        m_CurrentLaunchForce = Random.Range(m_MinLaunchForce, m_MaxLaunchForce);
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

    private void AimAtPlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - m_FireTransform.position;
        Quaternion currentRotation = m_FireTransform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        m_FireTransform.rotation = Quaternion.Euler(
            targetRotation.eulerAngles.x,
            currentRotation.eulerAngles.y,
            currentRotation.eulerAngles.z
        );
    }
}

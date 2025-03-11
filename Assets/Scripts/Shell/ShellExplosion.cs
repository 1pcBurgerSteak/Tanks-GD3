using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MinDamage = 25f;
    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 2f;
    public float m_ExplosionRadius = 5f;


    public string tagname; 
    private void Start()
    {
        // Destroy the shell after its lifetime ends
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagname))
        {
            float randomDamage = Random.Range(m_MinDamage, m_MaxDamage);

            TankHealth targetHealth = other.GetComponent<TankHealth>();
            if (targetHealth)
            {
                targetHealth.TakeDamage(randomDamage);
            }
        }

        // Play explosion effects
        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        //m_ExplosionAudio.Play();

        // Destroy the particle system after it finishes
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        // Destroy the shell
        Destroy(gameObject);
    }
}

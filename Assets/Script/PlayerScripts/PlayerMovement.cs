using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed = 12f; // Default speed
    public float m_TurnSpeed = 180f;
    private Rigidbody m_Rigidbody;
    public float m_MovementInputValue;
    public float m_TurnInputValue;

    public bool speedBuff = false;
    public bool scaleBuff = false;
    private bool isMoving = false;

    AudioManager audioManager;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        //audioManager = FindObjectOfType<AudioManager>();
    }

    
    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Update()
    {
        //MovingAudio();
    }

    public void UpdateMovement(Vector2 input)
    {
        m_MovementInputValue = input.y;
        m_TurnInputValue = input.x;
        
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    // Enable the speed buff
    public void EnableSpeedBuff()
    {
        CancelInvoke(nameof(DisableSpeedBuff)); // Cancel any existing timer
        speedBuff = true;
        m_Speed = 20f;

        Invoke(nameof(DisableSpeedBuff), 10f);
    }

    // Disable the speed buff
    public void DisableSpeedBuff()
    {
        if (speedBuff)
        {
            speedBuff = false;
            m_Speed = 12f;
        }
    }


    public void EnableScaleBuff()
    {
        CancelInvoke(nameof(DisableScaleBuff)); // Cancel any existing timer
        scaleBuff = true;
        gameObject.transform.localScale = new Vector3(2f, 2f, 2f);

        // Reset the scale after 15 seconds
        Invoke(nameof(DisableScaleBuff), 15f);
    }

    // Disable the scale buff
    public void DisableScaleBuff()
    {
        if (scaleBuff)
        {
            scaleBuff = false;
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            Debug.Log("potaka");
        }
    }

    //public void MovingAudio()
    //{
    //    if (Mathf.Abs(m_MovementInputValue) > 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
    //    {
    //        if(isMoving)
    //        {
    //            audioManager.PlaySFX("ShipMoving");
    //        } 
    //        isMoving = true;
    //    }
    //    else
    //    {
    //        audioManager.StopSFX();
    //        isMoving = false;
    //    }
    //}
}

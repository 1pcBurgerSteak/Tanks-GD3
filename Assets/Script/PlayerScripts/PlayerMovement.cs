using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    private Rigidbody m_Rigidbody;
    public float m_MovementInputValue;
    public float m_TurnInputValue;
    private void Awake ()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }
    public void UpdateMovement(Vector2 input)
    {
        m_MovementInputValue = input.y;
        m_TurnInputValue = input.x;
    }


    private void Start ()
    {

    }

    private void FixedUpdate ()
    {
        Move ();
        Turn ();
    }


    private void Move ()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn ()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
        m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
    }
}
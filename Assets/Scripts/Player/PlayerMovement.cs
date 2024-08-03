using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    private Transform m_tf;
    private Rigidbody m_rb;

    private float m_turnInput;
    private float m_accelerateInput;
    private float m_brakeInput;
    private Vector2 m_lookInput;

    private Vector3 m_deltaMove;
    private Quaternion m_deltaRotation;

    [SerializeField] private float m_speed = 5;
    [SerializeField] private float m_turnSpeed = 5;

    private void Awake()
    {
        m_tf = GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        m_deltaMove = m_tf.forward * m_speed * m_accelerateInput * Time.deltaTime;
        float deltaLocalAngle = m_turnInput * m_turnSpeed * (180 / Mathf.PI) * Time.deltaTime;
        Debug.Log(deltaLocalAngle);
        m_deltaRotation = Quaternion.AngleAxis(deltaLocalAngle, m_tf.up);
    }

    private void FixedUpdate()
    {
        m_rb.MovePosition(m_rb.position + m_deltaMove);
        m_rb.MoveRotation((m_rb.rotation * m_deltaRotation).normalized);
    }

    public void TurnInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Turn: " + context.ReadValue<float>());
        m_turnInput = context.ReadValue<float>();
    }

    public void AccelerateInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Accelerate: " + context.ReadValue<float>());
        m_accelerateInput = context.ReadValue<float>();
    }

    public void BrakeInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Brake: " + context.ReadValue<float>());
        m_brakeInput = context.ReadValue<float>();
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Look: " + context.ReadValue<Vector2>());
        m_lookInput = context.ReadValue<Vector2>();
    }
}

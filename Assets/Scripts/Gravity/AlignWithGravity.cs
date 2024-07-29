using UnityEngine;

[RequireComponent(typeof(GravityObject))]
[RequireComponent(typeof(Rigidbody))]
public class AlignWithGravity : MonoBehaviour
{
    private Transform m_tf;
    private Rigidbody m_rb;
    private GravityObject m_go;

    [SerializeField] private float m_threshold = 5;
    [SerializeField] private Vector3 m_offset;
    public Vector3 Offset { get { return m_offset; } set { m_offset = value; } }
    [SerializeField] private float m_lerpRate = 0.1f;
    [SerializeField] bool m_enabled = true;

    private Quaternion m_rotation;


    private void Awake()
    {
        m_tf = GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody>();
        m_go = GetComponent<GravityObject>();
    }

    private void Start()
    {
        m_rb.freezeRotation = true;
    }

    private void Update()
    {
        if (m_go.Gravity.magnitude < m_threshold) return;

        Vector3 targetUp = Quaternion.Euler(m_offset) * -m_go.Gravity;
        Vector3 lookDirection = Vector3.ProjectOnPlane(m_tf.forward, targetUp);

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, targetUp);
        m_rotation = Quaternion.Slerp(m_rb.rotation, targetRotation, m_lerpRate * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!m_enabled) return;

        m_rb.MoveRotation(m_rotation.normalized);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityObject : MonoBehaviour
{
    private Transform m_tf;
    private Rigidbody m_rb;
    private GravityManager m_gravityManager;

    private Vector3 m_gravity;
    public Vector3 Gravity { get { return m_gravity; } }

    [SerializeField] private bool m_useGravity = true;
    public bool UseGravity { get { return m_useGravity; } set { m_useGravity = value; } }

    [SerializeField] private bool m_alwaysShowGizmos;

    private void Awake()
    {
        m_tf = GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_rb.useGravity = false;
        m_gravityManager = GravityManager.Instance;
    }

    private void Update()
    {
        m_gravity = EvaluateGravity();
    }

    private void FixedUpdate()
    {
        if(m_useGravity)
        {
            m_rb.AddForce(m_gravity, ForceMode.Acceleration);
        }
    }

    private Vector3 EvaluateGravity()
    {
        Vector3 gravity = Vector3.zero;

        foreach (IGravityEmitter ge in m_gravityManager.GravityEmitters)
        {
            if (!ge.IsPointInRange(m_tf.position)) continue;

            gravity += ge.GetGravity(m_tf.position);
        }

        return gravity;
    }

    private void OnDrawGizmos()
    {
        if (m_alwaysShowGizmos) OnDrawGizmosSelected();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.DrawRay(transform.position, m_gravity);
    }


}

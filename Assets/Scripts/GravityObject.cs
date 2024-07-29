using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityObject : MonoBehaviour
{
    private Transform m_tf;
    private Rigidbody m_rb;
    private Collider m_collider;
    private Vector3 m_gravity;
    public Vector3 Gravity { get { return m_gravity; } }
    [SerializeField]
    private bool m_isGravity = true;
    public bool IsGravity { get { return m_isGravity; } set { m_isGravity = value; } }
    private GravityManager m_gravityManager; 

    private void Awake()
    {
        m_tf = GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
        m_gravityManager = GravityManager.Instance;
    }

    private void Update()
    {
        m_gravity = EvaluateGravity();
    }

    private void FixedUpdate()
    {
        if(m_isGravity)
        {
            m_rb.AddForce(m_gravity, ForceMode.Force);
        }
    }

    private Vector3 EvaluateGravity()
    {
        Vector3 gravity = Vector3.zero;
        
        if (m_collider == null)
        {
            foreach (IGravityEmitter ge in m_gravityManager.GravityEmitters)
            {
                if (!ge.PointInRange(m_tf.position)) continue;

                gravity += ge.GetGravity(m_tf.position);
            }
        } else
        {
            foreach (IGravityEmitter ge in m_gravityManager.GravityEmitters)
            {
                if (!ge.ColliderIntersectsRange(m_collider)) continue;

                gravity += ge.GetGravity(m_tf.position);
            }
        }

        return gravity;
    }


}

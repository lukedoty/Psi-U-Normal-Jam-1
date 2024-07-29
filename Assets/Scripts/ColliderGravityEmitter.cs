using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderGravityEmitter : MonoBehaviour, IGravityEmitter
{
    private Collider m_collider;

    [SerializeField]
    private float m_force = 9.81f;
    public float Force { get { return m_force; } set { m_force = value; } }

    [SerializeField]
    private Vector3 m_direction;
    public Vector3 Direction { get { return m_direction; } set { m_direction = value; } }

    [SerializeField]
    private bool m_alwaysShowGizmos;
    
    private void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

    private void Start()
    {
        ((IGravityEmitter)this).AddToManager();
    }

    public bool PointInRange(Vector3 point)
    {
        Collider[] overlaps = Physics.OverlapSphere(point, 0, 1 << 6);

        foreach (Collider c in overlaps)
        {
            if (c == m_collider) return true;
        }

        return false;
    }

    public Vector3 GetGravity(Vector3 point)
    {
        return Quaternion.Euler(m_direction) * (m_force * Vector3.down);
    }

    private void OnDrawGizmos()
    {
        if (m_alwaysShowGizmos) OnDrawGizmosSelected();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.DrawRay(transform.position, GetGravity(transform.position));

        Gizmos.color = Color.blue;
        Collider collider = GetComponent<Collider>();
        Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
    }
}

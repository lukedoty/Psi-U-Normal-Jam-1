using UnityEngine;

public class BodyGravityEmitter : MonoBehaviour, IGravityEmitter
{
    private Transform m_tf;

    [SerializeField] private float m_baselineRadius = 20;
    [SerializeField] private float m_baselineForce = 9.81f;
    [SerializeField] private float m_forceCutoff = 0.1f;
    [SerializeField] private bool m_alwaysDrawGizmos;

    private void Awake()
    {
        m_tf = GetComponent<Transform>();
    }

    private void Start()
    {
        ((IGravityEmitter)this).AddToManager();
    }

    public bool IsPointInRange(Vector3 point) {
        float weightedMass = m_baselineForce * m_baselineRadius * m_baselineRadius;
        float range = Mathf.Sqrt(weightedMass / m_forceCutoff);
        return (m_tf.position - point).magnitude <= range;
    }

    public Vector3 GetGravity(Vector3 point)
    {
        Vector3 differrence = m_tf.position - point;
        Vector3 direction = differrence.normalized;
        float distance = Mathf.Max(differrence.magnitude, m_baselineRadius);

        float weightedMass = m_baselineForce * m_baselineRadius * m_baselineRadius;
        float force = weightedMass / (distance * distance);

        return direction * force;
    }

    private void OnDrawGizmos()
    {
        if (m_alwaysDrawGizmos) OnDrawGizmosSelected();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        float weightedMass = m_baselineForce * m_baselineRadius * m_baselineRadius;
        float range = Mathf.Sqrt(weightedMass / m_forceCutoff);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, m_baselineRadius);
        
    }
}

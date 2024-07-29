using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravityEmitter : MonoBehaviour, IGravityEmitter
{
    private Transform m_tf;
    [SerializeField]
    private float m_gravity = 9.81f;

    private void Awake()
    {
        m_tf = GetComponent<Transform>();
    }

    private void Start()
    {
        ((IGravityEmitter)this).AddToManager();
    }

    public bool PointInRange(Vector3 point) { return true; }
    public bool ColliderInRange(Collider collider) { return true; }
    public bool ColliderIntersectsRange(Collider collider) { return true; }

    public Vector3 GetGravity(Vector3 point)
    {
        return m_tf.rotation * (m_gravity * Vector3.down);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.rotation * (m_gravity * Vector3.down));
    }
}

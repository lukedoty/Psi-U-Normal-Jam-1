using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravityEmitter : MonoBehaviour, IGravityEmitter
{
    [SerializeField]
    private float m_force = 9.81f;
    public float Force { get { return m_force; } set { m_force = value; } }

    [SerializeField]
    private Vector3 m_direction;
    public Vector3 Direction { get { return m_direction; } set { m_direction = value; } }

    private void Start()
    {
        ((IGravityEmitter)this).AddToManager();
    }

    public bool PointInRange(Vector3 point) { return true; }

    public Vector3 GetGravity(Vector3 point)
    {
        return Quaternion.Euler(m_direction) * (m_force * Vector3.down);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, GetGravity(transform.position));
    }
}

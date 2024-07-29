using UnityEngine;

interface IGravityEmitter
{
    bool PointInRange(Vector3 point);
    bool ColliderInRange(Collider collider);
    bool ColliderIntersectsRange(Collider collider);
    Vector3 GetGravity(Vector3 point);
}

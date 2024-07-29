using UnityEngine;

public interface IGravityEmitter
{
    void AddToManager() { GravityManager.Instance.GravityEmitters.Add(this); }
    bool PointInRange(Vector3 point);
    bool ColliderInRange(Collider collider);
    bool ColliderIntersectsRange(Collider collider);
    Vector3 GetGravity(Vector3 point);
}

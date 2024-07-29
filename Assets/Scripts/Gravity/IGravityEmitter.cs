using UnityEngine;

public interface IGravityEmitter
{
    void AddToManager() { GravityManager.Instance.GravityEmitters.Add(this); }
    bool IsPointInRange(Vector3 point);
    Vector3 GetGravity(Vector3 point);
}

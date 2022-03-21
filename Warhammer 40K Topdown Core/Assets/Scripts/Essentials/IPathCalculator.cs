using UnityEngine;

public interface IPathCalculator
{
    bool AgentIsStopped { get; }

    float GetDistance(Vector3 position);
    float RemainingDistance { get; }
    bool IsPathCalculated { get; }

    void ResetPath();
    void SetDestination(Vector3 position);
    void FreezeAgent();
    void ResetAgent();
}
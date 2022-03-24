using UnityEngine;
using UnityEngine.AI;

public interface IPathCalculator
{
    bool AgentIsStopped { get; }
    void SetEndPosition(Vector3 position);
    bool IsPathCalculated { get; }
    void ResetPath();
    void SetDestination(Vector3 position);
    void FreezeAgent();
    void ResetAgent();
    void SetMoveDistance(float distance);
    Vector3 GetEndPosition(Vector3 position);
}
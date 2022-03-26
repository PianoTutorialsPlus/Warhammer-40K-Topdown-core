using UnityEngine;

public class NavMeshPathPosition
{
    private static Vector3[] _pathCorners;
    private static Vector3 _pathPosition;
    private static float _moveRange;

    public NavMeshPathPosition(Vector3[] pathCorners, float range)
    {
        _pathCorners = pathCorners;
        _moveRange = range;
    }

    public Vector3 EndPosition
    {
        get => _pathPosition;
        set
        {
            _pathPosition = (_moveRange == 0)
                ? Vector3.zero
                : LocateAndSetEndPosition() != Vector3.zero
                    ? LocateAndSetEndPosition()
                    : value;
        }
    }
    private static Vector3 LocateAndSetEndPosition()
    {
        float lng = 0.0f;
        Vector3 position = Vector3.zero;

        for (int i = 1; i < _pathCorners.Length; ++i)
        {
            Vector3 lastPos = _pathCorners[i - 1];
            Vector3 currentPos = _pathCorners[i];

            position = LocateEndPosition(lastPos, currentPos, lng);
            lng += Vector3.Distance(lastPos, currentPos);

            if (position != Vector3.zero) break;
        }
        return position;
    }
    private static Vector3 LocateEndPosition(Vector3 lastPos, Vector3 currentPos, float lng)
    {
        Vector3 position = Vector3.zero;
   
        if (lng + Vector3.Distance(lastPos, currentPos) >= _moveRange)
        {
            Vector3 normalizedVector = (currentPos - lastPos).normalized;
            float delta = _moveRange - lng;
            position = lastPos + normalizedVector * delta;
        }
        return position;
    }
}
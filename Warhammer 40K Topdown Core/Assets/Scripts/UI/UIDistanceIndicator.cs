using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDistanceIndicator : MonoBehaviour
{
    float scale = 3.5f;
    float baseSize = 2.5f;

    public void ConnectIndicator(Unit unit)
    {
        transform.SetParent(unit.gameObject.transform);
        transform.position = unit.transform.position;
        SetActionRadius(unit);
    }

    public void SetActionRadius(Unit unit)
    {
        float actionRadiusXZ = (unit.restDistance + baseSize) * scale;
        Vector3 actionArea = new Vector3(actionRadiusXZ, 1, actionRadiusXZ);

        transform.localScale = actionArea;
    }
}

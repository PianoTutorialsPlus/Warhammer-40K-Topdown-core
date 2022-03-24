using System.Collections;
using UnityEngine;

public class UIDistanceIndicator : MonoBehaviour
{
    float scale = 3.5f;
    float baseSize = 0.5f;

    public void ConnectIndicator(Unit unit)
    {
        transform.SetParent(unit.gameObject.transform);
        transform.position = unit.transform.position;
        StartCoroutine(SetActionRadius(unit));
    }

    public IEnumerator SetActionRadius(Unit unit)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            float actionRadiusXZ = (unit.restDistance + baseSize) * scale;
            Vector3 actionArea = new Vector3(actionRadiusXZ, 1, actionRadiusXZ);

            transform.localScale = actionArea;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameInfoManager : MonoBehaviour
{
    [SerializeField] private UIGameInfoFiller infoItem;

    public void FillInfoPanel(Unit unit, PhaseSO phase, TurnSO turn)
    {
        infoItem.FillInfoPanel(unit,phase,turn);
    }
}

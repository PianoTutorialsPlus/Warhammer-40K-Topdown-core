using UnityEngine;

public class UIGameInfoManager : MonoBehaviour
{
    [SerializeField] private UIGameInfoFiller infoItem;

    //public void FillInfoPanel(Unit unit, PhaseSO phase, TurnSO turn)
    //{
    //    infoItem.FillInfoPanel(unit,phase,turn);
    //}

    public void FillInfoPanel(GameStatsSO gameStats)
    {
        infoItem.FillInfoPanel(gameStats);
    }
}

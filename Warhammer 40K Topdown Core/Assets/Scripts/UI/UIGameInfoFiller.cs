using TMPro;
using UnityEngine;
using WH40K.Essentials;

public class UIGameInfoFiller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoUnitName;
    [SerializeField] TextMeshProUGUI infoPhase;
    [SerializeField] TextMeshProUGUI infoTurn;

    //public void FillInfoPanel(Unit unit,PhaseSO phase, TurnSO turn)
    //{
    //    infoUnitName.text = unit.tag;
    //    infoPhase.text = phase.phase.ToString();
    //    infoTurn.text = "Turn: " + turn.turn.ToString();
    //}
    public void FillInfoPanel(GameStatsSO gameStats)
    {
        infoUnitName.text = gameStats.ActivePlayer.PlayerUnits[0].tag;
        infoPhase.text = gameStats.phase.ToString();
        infoTurn.text = "Turn: " + gameStats.turn.ToString();
    }

}

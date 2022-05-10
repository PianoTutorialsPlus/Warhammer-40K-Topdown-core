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
    public void FillInfoPanel()
    {
        infoUnitName.text = GameStats.ActivePlayer.PlayerUnits[0].tag;
        infoPhase.text = GameStats.Phase.ToString();
        infoTurn.text = "Turn: " + GameStats.Turn.ToString();
    }

}

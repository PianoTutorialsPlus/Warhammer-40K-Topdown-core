using UnityEngine;
using TMPro;

public class UIGameInfoFiller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoUnitName;
    [SerializeField] TextMeshProUGUI infoPhase;
    [SerializeField] TextMeshProUGUI infoTurn;
    
    public void FillInfoPanel(Unit unit,PhaseSO phase, TurnSO turn)
    {
        infoUnitName.text = unit.tag;
        infoPhase.text = phase.phase.ToString();
        infoTurn.text = "Turn: " + turn.turn.ToString();
    }
}

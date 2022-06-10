using TMPro;
using UnityEngine;
using WH40K.Stats;

namespace WH40K.UI
{
    public class UIGameInfoFiller : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI infoUnitName;
        [SerializeField] TextMeshProUGUI infoPhase;
        [SerializeField] TextMeshProUGUI infoTurn;
        [SerializeField] GameStatsSO _gameStats;

        public void FillInfoPanel()
        {
            infoUnitName.text = _gameStats.ActivePlayer.PlayerUnits[0].tag;
            infoPhase.text = _gameStats.Phase.ToString();
            infoTurn.text = "Turn: " + _gameStats.Turn.ToString();
        }

    }
}
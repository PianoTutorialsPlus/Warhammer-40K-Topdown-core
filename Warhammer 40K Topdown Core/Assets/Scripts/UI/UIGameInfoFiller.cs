using TMPro;
using UnityEngine;
using WH40K.Gameplay.Core;

namespace WH40K.UI
{
    public class UIGameInfoFiller : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI infoUnitName;
        [SerializeField] TextMeshProUGUI infoPhase;
        [SerializeField] TextMeshProUGUI infoTurn;

        public void FillInfoPanel()
        {
            infoUnitName.text = GameStats.ActivePlayer.PlayerUnits[0].tag;
            infoPhase.text = GameStats.Phase.ToString();
            infoTurn.text = "Turn: " + GameStats.Turn.ToString();
        }

    }
}
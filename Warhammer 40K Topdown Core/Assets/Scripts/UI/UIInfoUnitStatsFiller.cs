using TMPro;
using UnityEngine;
using WH40K.PlayerEvents;

namespace WH40K.UI
{
    public class UIInfoUnitStatsFiller : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI infoUnitName;
        [SerializeField] TextMeshProUGUI infoUnitStats;
        [SerializeField] TextMeshProUGUI infoWeaponName;
        [SerializeField] TextMeshProUGUI infoWeaponStats;

        public void FillInfoPanel(IStats unit)
        {
            infoUnitName.text = unit.WeaponName;
            infoUnitStats.text = unit.Movement.ToString();
            infoWeaponName.text = unit.WeaponName;
            infoWeaponStats.text = unit.WeaponRange.ToString();
        }
    }
}
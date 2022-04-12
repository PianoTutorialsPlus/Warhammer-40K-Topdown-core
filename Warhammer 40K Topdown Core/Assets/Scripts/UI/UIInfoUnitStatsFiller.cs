using TMPro;
using UnityEngine;
using WH40K.Essentials;

public class UIInfoUnitStatsFiller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoUnitName;
    [SerializeField] TextMeshProUGUI infoUnitStats;
    [SerializeField] TextMeshProUGUI infoWeaponName;
    [SerializeField] TextMeshProUGUI infoWeaponStats;

    public void FillInfoPanel(IStats unit)
    {
        infoUnitName.text = unit.WeaponName;
        infoUnitStats.text = unit.Movement.ToString();//"Necron Stats";
        infoWeaponName.text = unit.WeaponName;//activeUnit.activeUnit._weaponSO.name;
        infoWeaponStats.text = unit.WeaponRange.ToString(); //"Weapon Stats";
    }
}

using UnityEngine;
using TMPro;

public class UIInfoUnitStatsFiller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoUnitName;
    [SerializeField] TextMeshProUGUI infoUnitStats;
    [SerializeField] TextMeshProUGUI infoWeaponName;
    [SerializeField] TextMeshProUGUI infoWeaponStats;
    
    public void FillInfoPanel(Unit unit)
    {
        infoUnitName.text = unit.name;
        infoUnitStats.text = unit._unitSO.Movement.ToString();//"Necron Stats";
        infoWeaponName.text = unit._weaponSO.name;//activeUnit.activeUnit._weaponSO.name;
        infoWeaponStats.text = unit._weaponSO.Range.ToString(); //"Weapon Stats";
    }
}

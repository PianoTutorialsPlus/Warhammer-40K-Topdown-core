using UnityEngine;
using TMPro;

public class UIInfoUnitStatsFiller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoUnitName;
    [SerializeField] TextMeshProUGUI infoUnitStats;
    [SerializeField] TextMeshProUGUI infoWeaponName;
    [SerializeField] TextMeshProUGUI infoWeaponStats;
    [SerializeField] ActiveUnitSO activeUnit;

    public void FillInfoPanel(Unit unit)
    {
        infoUnitName.text = unit.name;
        infoUnitStats.text = "Necron Stats";
        infoWeaponName.text = unit._weaponSO.name;//activeUnit.activeUnit._weaponSO.name;
        infoWeaponStats.text = "Weapon Stats";
        //interactionName = interactionItem.InteractionName;
        //interactionKeyButton.text = KeyCode.E.ToString(); // this keycode will be modified later on 
    }
}

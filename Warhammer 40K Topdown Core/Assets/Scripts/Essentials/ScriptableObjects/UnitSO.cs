using UnityEngine;

public abstract class UnitSO : ScriptableObject
{
    [Tooltip("Name of the Unit")]
    [SerializeField] private string _name = default;

    [Tooltip("Movementrange")]
    [SerializeField] private int _movement = default;

    [Tooltip("Weapon Skill")]
    [SerializeField] private int _weaponSkill = default;

    [Tooltip("Ballistic Skill")]
    [SerializeField] private int _ballisticSkill = default;

    [Tooltip("Strength of the Unit")]
    [SerializeField] private int _strength = default;

    [Tooltip("Toughness of the Unit")]
    [SerializeField] private int _toughness = default;

    [Tooltip("Wounds / Livepoints")]
    [SerializeField] private int _wounds = default;

    [Tooltip("Attacks of the Unit")]
    [SerializeField] private int _attack = default;

    [Tooltip("Leadership")]
    [SerializeField] private int _leadership = default;

    [Tooltip("Armour Save")]
    [SerializeField] private int _armourSave = default;


    public new string name { get => _name; }

    public int Movement { get => _movement; }

    public int WeaponSkill { get => _weaponSkill; }
    public int Wounds { get => _wounds - takenWounds; set => takenWounds += value; }
    public int Attacks { get => _attack; }

    public int BallisticSkill { get => _ballisticSkill; }

    public int Strength { get => _strength; }

    public int Toughness { get => _toughness; }

    public int Leadership { get => _leadership; }
    public int ArmourSave { get => _armourSave; }

    [HideInInspector] public int takenWounds;
}





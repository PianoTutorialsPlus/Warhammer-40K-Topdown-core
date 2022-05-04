using UnityEngine;
using WH40K.Essentials;

public abstract class UnitSO : ScriptableObject, IUnitStats
{
    [Tooltip("Name of the Unit")]
    [SerializeField] private string _name = default;

    [Tooltip("Fraction of the Unit")]
    [SerializeField] private Fraction _fraction = default;

    [Tooltip("Movement Range")]
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
    public Fraction Fraction { get => _fraction; protected set => _fraction = value; }
    public int Movement { get => _movement; protected set => _movement = value; }

    public int WeaponSkill => _weaponSkill;
    public int Wounds => _wounds;
    public int Attacks => _attack;
    public int BallisticSkill => _ballisticSkill;
    public int Strength => _strength;
    public int Toughness => _toughness;
    public int Leadership => _leadership;
    public int ArmourSave => _armourSave;

}





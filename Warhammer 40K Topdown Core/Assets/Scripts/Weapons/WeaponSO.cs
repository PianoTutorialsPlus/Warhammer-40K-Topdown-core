using UnityEngine;

[CreateAssetMenu(fileName = "WeaponVariant", menuName = "Weapons/WeaponVariant")]
public abstract class WeaponSO : ScriptableObject
{
    [Tooltip("Name of the Weapon")]
    [SerializeField] private string _name = default;

    [Tooltip("Range of the Weapon")]
    [SerializeField] private int _range;

    [Tooltip("Type of the Weapon")]
    [SerializeField] private WeaponType _type;

    [Tooltip("Amount of shots of the Weapon")]
    [SerializeField] private int _shots;

    [Tooltip("Strength of the Weapon")]
    [SerializeField] private int _strength;

    [Tooltip("Armour Penetration of the Weapon")]
    [SerializeField] private int _armourPen;

    [Tooltip("Damage of the Weapon")]
    [SerializeField] private int _damage;

    public new string name { get => _name; } //ENCAPSULATION
    public int Range { get => _range; }
    public WeaponType Type { get => _type; }
    public int Shots { get => _shots; }
    public int Strength { get => _strength; }
    public int ArmourPen { get => _armourPen; }
    public int Damage { get => _damage; }

    public enum WeaponType
    {
        RapidFire, Assault
    }

    public virtual int HitModifier(int hits) // INHARITANCE
    {
        return hits;
    }


}


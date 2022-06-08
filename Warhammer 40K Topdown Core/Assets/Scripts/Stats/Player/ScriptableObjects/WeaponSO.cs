using UnityEngine;

namespace WH40K.Stats.Player
{
    [CreateAssetMenu(fileName = "WeaponVariant", menuName = "Weapons/WeaponVariant")]
    public abstract class WeaponSO : ScriptableObject, IWeaponStats
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

        public string WeaponName => _name;  //ENCAPSULATION
        public int WeaponRange => _range;
        public WeaponType Type => _type;
        public int WeaponShots => _shots;
        public int WeaponStrength => _strength;
        public int WeaponArmourPen => _armourPen;
        public int WeaponDamage => _damage;

        public enum WeaponType
        {
            RapidFire, Assault
        }

        public virtual int HitModifier(int hits) // INHARITANCE
        {
            return hits;
        }


    }
}
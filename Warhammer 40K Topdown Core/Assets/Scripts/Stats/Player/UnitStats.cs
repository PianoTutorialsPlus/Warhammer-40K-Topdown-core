using System;
using Zenject;

namespace WH40K.Stats.Player
{
    public enum Fraction { None = 0, SpaceMarines, Necrons }
    public partial class UnitStats : IInitializable
    {
        private Settings _settings;
        private WeaponSO _weaponSO;
        private UnitSO _unitSO;

        private bool _done;
        private bool _activated;

        public UnitStats(
            Settings settings,
            WeaponSO weaponSO,
            UnitSO unitSO)
        {
            _settings = settings;
            _weaponSO = weaponSO;
            _unitSO = unitSO;
        }

        public void Initialize()
        {
            _done = _settings.Done;
            _activated = _settings.Activated;
        }

        public Fraction Fraction => _unitSO.Fraction;
        public int WeaponRange => _weaponSO.WeaponRange;
        public int WeaponShots => _weaponSO.WeaponShots;
        public int WeaponStrength => _weaponSO.WeaponStrength;
        public int WeaponArmourPen => _weaponSO.WeaponArmourPen;
        public int WeaponDamage => _weaponSO.WeaponDamage;
        public string WeaponName => _weaponSO.WeaponName;

        public int Movement => _unitSO.Movement;
        public int Wounds => _unitSO.Wounds;
        public int BallisticSkill => _unitSO.BallisticSkill;
        public int Toughness => _unitSO.Toughness;
        public int ArmourSave => _unitSO.ArmourSave;

        public bool IsDone => _done;
        public bool IsActivated => _activated;

        public void Activate()
        {
            _activated = true;
        }

        public void Freeze()
        {
            _done = true;
        }
        public void UnFreeze()
        {
            _done = false;
            _activated = false;
        }
        [Serializable]
        public class Settings
        {
            public bool CanMove;
            public bool Activated;
            public bool CanShoot;
            public bool Done;
            //public int woundsTaken;
        }
    }
}

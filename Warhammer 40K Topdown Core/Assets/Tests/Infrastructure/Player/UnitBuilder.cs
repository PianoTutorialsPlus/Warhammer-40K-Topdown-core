using NSubstitute;
using UnityEngine;
using UnityEngine.Events;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats.Player;

namespace Editor.Infrastructure.Player
{
    public class UnitBuilder : TestDataBuilder<IUnit>
    {
        private Fraction _fraction = Fraction.Necrons;
        private bool _isDone = false;
        private bool _isActivated = false;

        private UnityAction<IUnit> _onPointerEnterInfo;
        private UnityAction<IUnit> _onPointerExit;
        private UnityAction _onPointerEnter;
        private UnityAction<IUnit> _onTapDownAction;
        private UnitSelector _unitSelector;
        private int _intValue = 0;
        private int _wounds;
        private Vector3 _position;

        public UnitBuilder()
        {
        }

        public UnitBuilder WithFraction(Fraction fraction)
        {
            _fraction = fraction;
            return this;
        }
        public UnitBuilder WithUnitSelector(UnitSelector unitSelector)
        {
            _unitSelector = unitSelector;
            return this;
        }
        public UnitBuilder WithCurrentPosition(Vector3 position)
        {
            _position = position;
            return this;
        }
        public UnitBuilder WithIsActivatedState(bool isActivated)
        {
            _isActivated = isActivated;
            return this;
        }
        public UnitBuilder WithIsDoneState(bool isDone)
        {
            _isDone = isDone;
            return this;
        }
        public UnitBuilder WithOnPointerEnter(UnityAction onPointerEnter)
        {
            _onPointerEnter = onPointerEnter;
            return this;
        }
        public UnitBuilder WithOnPointerEnterInfo(UnityAction<IUnit> onPointerEnterInfo)
        {
            _onPointerEnterInfo = onPointerEnterInfo;
            return this;
        }
        public UnitBuilder WithOnPointerExit(UnityAction<IUnit> onPointerExit)
        {
            _onPointerExit = onPointerExit;
            return this;
        }
        public UnitBuilder WithOnTapdownAction(UnityAction<IUnit> onTapDownAction)
        {
            _onTapDownAction = onTapDownAction;
            return this;
        }
        public UnitBuilder WithInteger(int value)
        {
            _intValue = value;
            return this;
        }
        public UnitBuilder WithWounds(int value)
        {
            _wounds = value;
            return this;
        }
        public override IUnit Build()
        {
            var unit = Substitute.For<IUnit>();

            unit.Fraction.Returns(_fraction);
            unit.CurrentPosition.Returns(_position);
            //unit.UnitSelector.Returns(_unitSelector);
            // Condition
            unit.IsDone.Returns(_isDone);
            unit.IsActivated.Returns(_isActivated);
            // Pointer
            unit.OnPointerEnter.Returns(_onPointerEnter);
            unit.OnPointerEnterInfo.Returns(_onPointerEnterInfo);
            unit.OnPointerExit.Returns(_onPointerExit);
            unit.OnTapDownAction.Returns(_onTapDownAction);
            // Unit Stats
            unit.BallisticSkill.Returns(_intValue);
            unit.Wounds.Returns(_wounds);
            unit.Toughness.Returns(_intValue);
            unit.ArmourSave.Returns(_intValue);
            // Weapon Stats
            unit.WeaponShots.Returns(_intValue);
            unit.WeaponStrength.Returns(_intValue);
            unit.WeaponDamage.Returns(_intValue);

            return unit;
        }
    }
}

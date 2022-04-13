﻿using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using WH40K.Essentials;

namespace Editor.Infrastructure
{
    public class UnitBuilder : TestDataBuilder<IUnit>
    {
        private Fraction _fraction = Fraction.Necrons;
        private bool _isDone = false;
        private bool _isActivated = false;
        private UnityAction<IUnit> _onPointerEnterInfo;
        private UnityAction<IUnit> _onPointerExit;
        private UnityAction _onPointerEnter;

        public UnitBuilder()
        {
        }

        public UnitBuilder WithFraction(Fraction fraction)
        {
            _fraction = fraction;
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
        public override IUnit Build()
        {
            var unit = Substitute.For<IUnit>();

            unit.Fraction.Returns(_fraction);
            unit.IsDone.Returns(_isDone);
            unit.IsActivated.Returns(_isActivated);
            unit.OnPointerEnter.Returns(_onPointerEnter);
            unit.OnPointerEnterInfo.Returns(_onPointerEnterInfo);
            unit.OnPointerExit.Returns(_onPointerExit);

            return unit;
        }
    }
}
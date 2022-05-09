using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace WH40K.Essentials
{
    public abstract class UnitPhasesBase: MonoBehaviour
    {
        protected IUnit _unit;

        protected UnitSelector UnitSelector => Unit.UnitSelector;
        protected UnityAction<IUnit> onTapDownAction => Unit.OnTapDownAction;
        protected UnityAction onPointerEnter => Unit.OnPointerEnter;
        protected UnityAction<IUnit> onPointerEnterInfo => Unit.OnPointerEnterInfo;
        protected UnityAction<IUnit> onPointerExit => Unit.OnPointerExit;
        protected GameStatsSO _gameStats => Unit.GameStats;
        public IUnit ActiveUnit { get => _gameStats.ActiveUnit; set => _gameStats.ActiveUnit = value; }
        public IUnit Unit { get => _unit; protected set => _unit = value; }
    }
}

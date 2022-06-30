using Editor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats;
using WH40K.Stats.Player;

namespace Editor.Base
{
    public class CoreElementsBase
    {
        public UnityAction _pointerAction;
        public UnityAction<IUnit> _action;

        public IUnit GetUnit(
            UnityAction pointerWithArgument = null,
            UnityAction<IUnit> pointer = null,
            Fraction playerFraction = Fraction.Necrons,
            Vector3 currentPosition = default,
            int value = 0,
            int wounds = 0,
            bool isActivated = false, 
            bool isDone = false)
        {
            return A.Unit
                    .WithOnPointerEnterInfo(pointer)
                    .WithOnPointerEnter(pointerWithArgument)
                    .WithOnTapdownAction(pointer)
                    .WithOnPointerExit(pointer)
                    .WithFraction(playerFraction)
                    .WithCurrentPosition(currentPosition)
                    .WithInteger(value)
                    .WithWounds(wounds)
                    .WithIsActivatedState(isActivated)
                    .WithIsDoneState(isDone)
                    .Build();
        }
        public GameStatsSO GetGameStats(
            Fraction playerFraction = Fraction.Necrons, 
            Fraction enemyFraction = Fraction.None,
            IUnit unit = null)
        {
            return A.GameStats
                .WithActivePlayer(A.Player.WithFraction(playerFraction))
                .WithEnemyPlayer(A.Player.WithFraction(enemyFraction))
                .WithActiveUnit(unit ??= A.Unit.Build())
                .WithEnemyUnit(unit);
        }
    }
}

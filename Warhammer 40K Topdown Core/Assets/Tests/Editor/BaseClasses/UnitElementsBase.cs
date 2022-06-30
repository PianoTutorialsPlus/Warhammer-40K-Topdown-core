using Editor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats;
using WH40K.Stats.Player;

namespace Editor.Base
{
    public class UnitElementsBase : CoreElementsBase
    {
        public UnitSelector GetUnitSelector(
            Fraction playerFraction = Fraction.Necrons, 
            Fraction enemyFraction = Fraction.None, 
            GameStatsSO gameStats = null)
        {
            return A.UnitSelector
                .WithUnit(A.Unit.Build())
                .WithGameStats(gameStats)
                .WithPlayerFraction(playerFraction)
                .WithEnemyFraction(enemyFraction);
        }
        public MovementRange GetMovementRange(
            int maxRange = 0,
            Vector3 startPosition = new Vector3(),
            Vector3 currentPosition = new Vector3())
        {
            return A.MovementRange
                .WithMaxRange(maxRange)
                .WithStartPosition(startPosition)
                .WithCurrentPosition(currentPosition);
        }
    }
}

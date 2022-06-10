using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;
using WH40K.Stats.Combat;

namespace WH40K.Gameplay.Combat
{
    public class CalculateHits : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;

        private int ToHit => _gameStats.ActiveUnit.BallisticSkill;
        private int MaxShots => _gameStats.ActiveUnit.WeaponShots;

        public CalculateHits(IResult results, GameStatsSO gameStats) : base(results, gameStats) { }

        public override void Action(List<int> action)
        {
            OnEnable();
            var shots = new Shots(MaxShots);
            _diceAction.RaiseEvent(shots.GetShots());
        }
        public override void Result(List<int> hitResult)
        {
            if (hitResult == null || hitResult.Count == 0) return;

            Debug.Log("CalculateHitsSO Result");
            var combatResults = new CombatResults(ToHit, hitResult);

            _diceResult.RaiseEvent(combatResults.Hits);
        }
    }
}
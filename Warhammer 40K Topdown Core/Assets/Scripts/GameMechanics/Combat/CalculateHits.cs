using System.Collections.Generic;
using UnityEngine;

namespace WH40K.GameMechanics.Combat
{
    public class CalculateHits : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;

        private int ToHit => _gameStats.ActiveUnit.BallisticSkill;
        private int MaxShots => _gameStats.ActiveUnit.WeaponShots;

        public CalculateHits(IResult results) : base(results) { }

        public override void Action(List<int> action)
        {
            OnEnable();
            var shots = new Shots(MaxShots);
            DiceAction.RaiseEvent(shots.GetShots());
        }
        public override void Result(List<int> hitResult)
        {
            if (hitResult == null || hitResult.Count == 0) return;
            //if (diceEvent != ShootingSubEvents.Hit) return;

            Debug.Log("CalculateHitsSO Result");
            var combatResults = new CombatResults(ToHit, hitResult);

            DiceResult.RaiseEvent(combatResults.Hits);
        }
    }
}
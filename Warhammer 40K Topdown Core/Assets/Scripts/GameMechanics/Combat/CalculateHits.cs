using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
{
    public class CalculateHits : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;

        private int ToHit => GameStats.ActiveUnit.BallisticSkill;
        private int MaxShots => GameStats.ActiveUnit.WeaponShots;

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

            Debug.Log("CalculateHitsSO Result");
            var combatResults = new CombatResults(ToHit, hitResult);

            DiceResult.RaiseEvent(combatResults.Hits);
        }
    }
}
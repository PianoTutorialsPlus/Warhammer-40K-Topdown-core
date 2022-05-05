using System.Collections.Generic;
using UnityEngine;

namespace WH40K.GameMechanics.Combat
{
    public class CalculateWounds : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
        private WoundTable _woundTable;

        private int Strength => _gameStats.ActiveUnit.WeaponStrength;
        private int Toughness => _gameStats.EnemyUnit.Toughness;

        public CalculateWounds(IResult results) : base(results) { _woundTable = new WoundTable(); }

        public override void Action(List<int> hits)
        {
            if (hits == null || hits.Count == 0) return;

            Debug.Log("CalculateWoundsSO");
            DiceAction.RaiseEvent(ShootingSubEvents.Wound, hits);
        }
        public override void Result(ShootingSubEvents diceEvent, List<int> woundResult)
        {

            if (woundResult == null || woundResult.Count == 0) return;
            if (diceEvent != ShootingSubEvents.Wound) return;

            Debug.Log("CalculateWoundsSO Result");
            var combatResults = new CombatResults(_woundTable.ToWound(Strength, Toughness), woundResult);

            DiceResult.RaiseEvent(diceEvent, combatResults.Wounds);
        }
    }
}
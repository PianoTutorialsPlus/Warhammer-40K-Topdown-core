using System.Collections.Generic;
using UnityEngine;
using WH40K.Core;
using WH40K.EventChannels;
using WH40K.GamePhaseEvents;

namespace WH40K.GameMechanics.Combat
{
    public class CalculateWounds : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
        private WoundTable _woundTable;

        private int Strength => GameStats.ActiveUnit.WeaponStrength;
        private int Toughness => GameStats.EnemyUnit.Toughness;

        public CalculateWounds(IResult results) : base(results) { _woundTable = new WoundTable(); }

        public override void Action(List<int> hits)
        {
            if (hits == null || hits.Count == 0) return;
            OnEnable();
            Debug.Log("CalculateWoundsSO");
            _diceAction.RaiseEvent(hits);
        }
        public override void Result(List<int> woundResult)
        {
            if (woundResult == null || woundResult.Count == 0) return;
            //if (diceEvent != ShootingSubEvents.Wound) return;

            Debug.Log("CalculateWoundsSO Result");
            var combatResults = new CombatResults(_woundTable.ToWound(Strength, Toughness), woundResult);

            _diceResult.RaiseEvent(combatResults.Wounds);
        }
    }
}
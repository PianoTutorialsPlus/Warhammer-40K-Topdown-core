using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;
using WH40K.Stats.Combat;
using Zenject;

namespace WH40K.Gameplay.Combat
{
    public class CalculateWounds : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
        private WoundTable _woundTable;

        private int Strength => _gameStats.ActiveUnit.WeaponStrength;
        private int Toughness => _gameStats.EnemyUnit.Toughness;

        public CalculateWounds(IResult results, GameStatsSO gameStats) : base(results, gameStats) { _woundTable = new WoundTable(); }

        public CalculateWounds()
        {
        }

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
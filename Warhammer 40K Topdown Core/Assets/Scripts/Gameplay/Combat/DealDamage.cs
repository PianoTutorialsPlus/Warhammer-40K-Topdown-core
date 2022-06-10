using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;
using WH40K.Stats.Combat;

namespace WH40K.Gameplay.Combat
{
    public class DealDamage : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;

        private int Damage => _gameStats.ActiveUnit.WeaponDamage;

        public DealDamage(IResult results, GameStatsSO gameStats) : base(results, gameStats) { }

        public override void Action(List<int> notSaved)
        {
            if (notSaved == null || notSaved.Count == 0) return;
            OnEnable();
            var wounds = new Wounds(notSaved);
            var damage = wounds.TakeDamage(Damage);
            _gameStats.EnemyUnit.TakeDamage(damage);

            Result();
        }
        public override void Result(List<int> result = null)
        {
            //if (diceEvent != ShootingSubEvents.Damage) return;
            Debug.Log("DealDamage Result");
            _diceResult.RaiseEvent(result);
        }
    }
}
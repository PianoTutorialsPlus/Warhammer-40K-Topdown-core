using System.Collections.Generic;
using UnityEngine;

namespace WH40K.GameMechanics.Combat
{
    public class DealDamage : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;

        private int Damage => _gameStats.ActiveUnit.WeaponDamage;

        public DealDamage(IResult results) : base(results) { }

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
            DiceResult.RaiseEvent(result);
        }
    }
}
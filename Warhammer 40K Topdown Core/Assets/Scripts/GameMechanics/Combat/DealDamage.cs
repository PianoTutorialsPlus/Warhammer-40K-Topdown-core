using System.Collections.Generic;

namespace WH40K.GameMechanics.Combat
{
    public class DealDamage : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;

        private int Damage => _gameStats.ActiveUnit.WeaponDamage;

        public DealDamage(IResult results) : base(results) { }

        public override void Action(List<int> notSaved)
        {
            var wounds = new Wounds(notSaved);
            var damage = wounds.TakeDamage(Damage);
            _gameStats.EnemyUnit.TakeDamage(damage);

            Result(ShootingSubEvents.Damage);
        }
        public override void Result(ShootingSubEvents diceEvent, List<int> result = null)
        {
            if (diceEvent != ShootingSubEvents.Damage) return;
            DiceResult.RaiseEvent(diceEvent, result);
        }
    }
}
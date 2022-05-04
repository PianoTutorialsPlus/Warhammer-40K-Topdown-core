using System.Collections.Generic;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
{
    public class DealDamage : ICalculation
    {

        private readonly IResult _results;
        private RollTheDiceSO DiceResult => _results.DiceResult;

        private GameStatsSO _gameStats => _results.GameStats;
        private int Damage => _gameStats.ActiveUnit.WeaponDamage;

        public DealDamage(IResult results)
        {
            _results = results;
        }

        public void Action(List<int> notSaved)
        {
            var wounds = new Wounds(notSaved);
            var damage = wounds.TakeDamage(Damage);
            _gameStats.EnemyUnit.TakeDamage(damage);

            Result(ShootingSubEvents.Damage);
        }
        public void Result(ShootingSubEvents diceEvent, List<int> result = null)
        {
            DiceResult.RaiseEvent(diceEvent, result);
        }
    }
}
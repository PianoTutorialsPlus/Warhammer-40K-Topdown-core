using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
{
    public class DealDamage : ICalculation
    {
        private GameStatsSO _gameStats => _results.GameStats;
        private readonly IResult _results;

        private Wounds _wounds;
        private int Damage => _gameStats.ActiveUnit.WeaponDamage;
        private int WoundsLeft
        {
            get => _gameStats.EnemyUnit.Wounds;
            set => _gameStats.EnemyUnit.Wounds = value;
        }
        private RollTheDiceSO DiceResult => _results.DiceResult;


        public DealDamage(IResult results)
        {
            _results = results;
        }

        public void Action(List<int> notSaved)
        {
            _wounds = new Wounds(notSaved);
            WoundsLeft = _wounds.TakeDamage(Damage);

            if (WoundsLeft <= 0)
                _gameStats.EnemyUnit.Destroy();

            Result(ShootingSubEvents.Damage);
        }
        public void Result(ShootingSubEvents diceEvent, List<int> result = null)
        {
            DiceResult.RaiseEvent(diceEvent, result);
        }
    }
}
using System.Collections.Generic;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
{
    public abstract class CombatPhases
    {
        protected readonly IResult _results;
        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        protected RollTheDiceSO DiceSubResult => _results.DiceSubResult;
        protected RollTheDiceSO DiceAction => _results.DiceAction;
        protected RollTheDiceSO DiceResult => _results.DiceResult;
        protected GameStatsSO _gameStats => _results.GameStats;
        protected CombatPhases(IResult results)
        {
            _results = results;
            OnEnable();
        }
        public abstract void Action(List<int> action);
        public abstract void Result(ShootingSubEvents diceEvent, List<int> hitResult);
        private void OnEnable()
        {
            if (DiceSubResult != null) DiceSubResult.OnEventRaised += Result;
        }
    }
}
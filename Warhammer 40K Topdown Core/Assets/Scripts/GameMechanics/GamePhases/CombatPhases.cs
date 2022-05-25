using System.Collections.Generic;
using UnityEngine;
using WH40K.EventChannels;

namespace WH40K.GamePhaseEvents
{
    public abstract class CombatPhases
    {
        protected readonly IResult _results;
        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        protected RollTheDiceEventChannelSO _diceSubResult => _results.DiceSubResult;
        protected RollTheDiceEventChannelSO _diceAction => _results.DiceAction;
        protected RollTheDiceEventChannelSO _diceResult => _results.DiceResult;
        protected CombatPhases(IResult results)
        {
            _results = results;
        }
        protected void OnEnable()
        {
            Debug.Log("Enabled in " + SubEvents);
            if (_diceSubResult != null) _diceSubResult.OnEventRaised += Result;
        }
        public void Next()
        {
            Debug.Log("Disabled in " + SubEvents);
            if (_diceSubResult != null) _diceSubResult.OnEventRaised -= Result;
        }
        public abstract void Action(List<int> action);
        public abstract void Result(List<int> hitResult);

    }
}
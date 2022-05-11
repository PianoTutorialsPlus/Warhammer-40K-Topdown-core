using System.Collections.Generic;
using UnityEngine;
using WH40K.EventChannels;

namespace WH40K.GamePhaseEvents
{
    public abstract class CombatPhases
    {
        protected readonly IResult _results;
        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        protected RollTheDiceEventChannelSO DiceSubResult => _results.DiceSubResult;
        protected RollTheDiceEventChannelSO DiceAction => _results.DiceAction;
        protected RollTheDiceEventChannelSO DiceResult => _results.DiceResult;
        protected CombatPhases(IResult results)
        {
            _results = results;
        }
        public void Next()
        {
            Debug.Log("Disabled in " + SubEvents);
            if (DiceSubResult != null) DiceSubResult.OnEventRaised -= Result;
        }
        public abstract void Action(List<int> action);
        public abstract void Result(List<int> hitResult);
        protected void OnEnable()
        {
            Debug.Log("Enabled in " + SubEvents);
            if (DiceSubResult != null) DiceSubResult.OnEventRaised += Result;
        }
    }
}
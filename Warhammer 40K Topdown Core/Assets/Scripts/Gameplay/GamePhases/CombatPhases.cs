﻿using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Stats;
using Zenject;

namespace WH40K.Gameplay.GamePhaseEvents
{
    public abstract class CombatPhases
    {
        protected GameStatsSO _gameStats;
        protected IResult _results;
        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        protected RollTheDiceEventChannelSO _diceSubResult => _results.DiceSubResult;
        protected RollTheDiceEventChannelSO _diceAction => _results.DiceAction;
        protected RollTheDiceEventChannelSO _diceResult => _results.DiceResult;
        protected CombatPhases(
            IResult results, 
            GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            _results = results;
        }

        protected CombatPhases()
        {
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
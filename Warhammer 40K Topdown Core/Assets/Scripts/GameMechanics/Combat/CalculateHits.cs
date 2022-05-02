using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
{
    public class CalculateHits : ICalculation
    {
        private readonly IResult _results;
        private RollTheDiceSO DiceSubResult => _results.DiceSubResult;
        private RollTheDiceSO DiceAction => _results.DiceAction;
        private RollTheDiceSO DiceResult => _results.DiceResult;
       
        private GameStatsSO _gameStats => _results.GameStats;
        private int ToHit => _gameStats.ActiveUnit.BallisticSkill;
        private int MaxShots => _gameStats.ActiveUnit.WeaponShots;

        public CalculateHits(IResult results)
        {
            _results = results;
            OnEnable();
        }
        private void OnEnable()
        {
            if (DiceSubResult != null) DiceSubResult.OnEventRaised += Result;
        }
        public void Action(List<int> action)
        {
            var shots = new Shots(MaxShots);
            DiceAction.RaiseEvent(ShootingSubEvents.Hit, shots.GetShots());
        }
        public void Result(ShootingSubEvents diceEvent, List<int> hitResult)
        {
            if (hitResult == null || hitResult.Count == 0) return;
            if (diceEvent != ShootingSubEvents.Hit) return;

            Debug.Log("CalculateHitsSO Result");
            var combatResults = new CombatResults(ToHit, hitResult);

            DiceResult.RaiseEvent(diceEvent, combatResults.Hits);
        }


        //public override void Result(ShootingSubEvents diceEvent, List<int> hitResult)
        //{
        //    List<int> hits = new List<int>();
        //    if (diceEvent == ShootingSubEvents.Hit)
        //    {
        //        if (hitResult != null)
        //        {

        //            foreach (int result in hitResult)
        //            {
        //                Debug.Log("Hit result: " + result);
        //                if (result >= toHit)
        //                    hits.Add(result);
        //            }
        //            rollDiceResult.RaiseEvent(ShootingSubEvents.Hit, hits);

        //        }
        //    }
        //}
        //public RollTheDiceSO rollDices;
        //public RollTheDiceSO rollDicesResult;

        //List<int> hitResult = new List<int>();

        //private void OnEnable()
        //{
        //    //if (rollDicesResult != null) rollDicesResult.OnEventRaised += Result;
        //}

        //public List<int> HandleToHit(GameStatsSO gameStats)
        //{
        //    List<int> shots = new List<int>();
        //    List<int> hits = new List<int>();


        //    int toHit = gameStats.activeUnit._unitSO.BallisticSkill;

        //    for (int shot = 0; shot < gameStats.activeUnit._weaponSO.Shots; shot++)
        //    {
        //        shots.Add(shot);
        //        //Debug.Log(shot);
        //    }

        //    rollDices.RaiseEvent(DiceEvent.HitEvent, shots);
        //    //hitResult = rollDices.RollTheDice(shots);

        //    if (hitResult != null)
        //    {
        //        foreach (int result in hitResult)
        //        {
        //            //Debug.Log("Hit result: " + result);
        //            if (result >= toHit)
        //                hits.Add(result);
        //        }
        //        return hits;
        //    }
        //    return null;
        //}

        //private void Result(DiceEvent diceEvent,List<int> result)
        //{
        //    if (diceEvent == DiceEvent.HitEvent) hitResult = result;
        //}
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace WH40K.Combat
{
    public class CalculateWounds : ICalculation
    {
        private GameStatsSO _gameStats => _results.GameStats;
        private readonly IResult _results;
        private WoundTable woundTable;
        private CombatResults _combatResults;

        private RollTheDiceSO DiceSubResult => _results.DiceSubResult;
        private RollTheDiceSO DiceAction => _results.DiceAction;
        private RollTheDiceSO DiceResult => _results.DiceResult;

        private int Strength => _gameStats.activeUnit._weaponSO.Strength;
        private int Toughness => _gameStats.enemyUnit._unitSO.Toughness;

        public CalculateWounds(IResult result)
        {
            _results = result;
            OnEnable();
        }
        private void OnEnable()
        {
            if (DiceSubResult != null) DiceSubResult.OnEventRaised += Result;
        }
        public void Action(List<int> hits)
        {
            if (hits == null || hits.Count == 0) return;
            woundTable = new WoundTable();

            Debug.Log("CalculateWoundsSO");

            DiceAction.RaiseEvent(ShootingSubEvents.Wound, hits);
        }
        public void Result(ShootingSubEvents diceEvent, List<int> woundResult)
        {

            if (woundResult == null || woundResult.Count == 0) return;
            if (diceEvent != ShootingSubEvents.Wound) return;

            Debug.Log("CalculateWoundsSO Result");
            _combatResults = new CombatResults(woundTable.ToWound(Strength, Toughness), woundResult);

            DiceResult.RaiseEvent(diceEvent, _combatResults.Wounds);
        }



        //public override void Result(ShootingSubEvents diceEvent, List<int> woundResult)
        //{
        //    List<int> wounds = new List<int>();

        //    Debug.Log("CalculateWoundsSO Result");

        //    if (woundResult == null) return;

        //    if (diceEvent == ShootingSubEvents.Wound)
        //    {
        //        if (woundResult != null)
        //        {

        //            foreach (int result in woundResult)
        //            {
        //                Debug.Log("Wounds: " + result);
        //                if (result >= toWound)
        //                {
        //                    Debug.Log("toWounds: " + toWound);
        //                    wounds.Add(result);
        //                }
        //            }
        //            rollDiceResult.RaiseEvent(ShootingSubEvents.Wound, wounds);

        //        }
        //    }
        //}
        //public List<int> HandleToWound(List<int> hits, GameStatsSO gameStats)
        //{
        //    List<int> wounds = new List<int>();
        //    List<int> woundResult = new List<int>();

        //    int toWound = dataTable.WoundTable(gameStats.activeUnit._weaponSO.Strength, gameStats.enemyUnit._unitSO.Toughness);

        //    woundResult = rollDices.RollTheDice(hits);

        //    foreach (int result in woundResult)
        //    {
        //        Debug.Log("Wounds: " + result);
        //        if (result >= toWound)
        //            wounds.Add(result);
        //    }
        //    return wounds;
        //}
    }
}
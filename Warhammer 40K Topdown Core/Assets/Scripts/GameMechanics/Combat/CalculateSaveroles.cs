using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
{
    public class CalculateSaveroles : ICalculation
    {
        private readonly IResult _results;
        private RollTheDiceSO DiceSubResult => _results.DiceSubResult;
        private RollTheDiceSO DiceAction => _results.DiceAction;
        private RollTheDiceSO DiceResult => _results.DiceResult;

        private GameStatsSO _gameStats => _results.GameStats;
        private int Saves => _gameStats.EnemyUnit.ArmourSave;
        private int Modifier => _gameStats.ActiveUnit.WeaponArmourPen;
        private int ModifiedSaves => Saves - Modifier;

        public CalculateSaveroles(IResult results)
        {
            _results = results;
            OnEnable();
        }

        private void OnEnable()
        {
            if (DiceSubResult != null) DiceSubResult.OnEventRaised += Result;
        }

        public void Action(List<int> wounds)
        {
            DiceAction.OnEventRaised(ShootingSubEvents.Save, wounds);
        }

        public void Result(ShootingSubEvents diceEvent, List<int> saveResult)
        {
            if (saveResult == null || saveResult.Count == 0) return;
            if (diceEvent != ShootingSubEvents.Save) return;

            Debug.Log("CalculateSavesSO Result");
            var combatResults = new CombatResults(ModifiedSaves, saveResult);

            DiceResult.RaiseEvent(ShootingSubEvents.Save, combatResults.FailedSaves);
        }

        //public override void Result(ShootingSubEvents diceEvent, List<int> SaveResult)
        //{
        //    List<int> notSaved = new List<int>();
        //    if (diceEvent == ShootingSubEvents.Save)
        //    {
        //        if (SaveResult != null)
        //        {

        //            foreach (int result in SaveResult)
        //            {
        //                Debug.Log("saveResult: " + result);
        //                if (result < (saves - modifier))
        //                    notSaved.Add(result);
        //            }
        //            rollDiceResult.RaiseEvent(ShootingSubEvents.Save, notSaved);
        //        }
        //    }
        //}
        //public List<int> HandleSaveRoles(List<int> wounds, GameStatsSO gameStats)
        //{
        //    List<int> saveResult = new List<int>();
        //    List<int> notSaved = new List<int>();

        //    int saves = gameStats.enemyUnit._unitSO.ArmourSave;
        //    int modifier = gameStats.activeUnit._weaponSO.ArmourPen;

        //    saveResult = rollDices.RollTheDice(wounds);

        //    foreach (int result in saveResult)
        //    {
        //        Debug.Log("saveResult: " + result);
        //        if (result < (saves - modifier))
        //            notSaved.Add(result);
        //    }
        //    return notSaved;
        //}
    }
}
using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
{
    public class CalculateSaveroles : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
        private int Saves => GameStats.EnemyUnit.ArmourSave;
        private int Modifier => GameStats.ActiveUnit.WeaponArmourPen;
        private int ModifiedSaves => Saves - Modifier;

        public CalculateSaveroles(IResult results) : base(results) { }

        public override void Action(List<int> wounds)
        {
            if (wounds == null || wounds.Count == 0) return;
            OnEnable();
            DiceAction.RaiseEvent(wounds);
        }

        public override void Result(List<int> saveResult)
        {
            if (saveResult == null || saveResult.Count == 0) return;
            //if (diceEvent != ShootingSubEvents.Save) return;

            Debug.Log("CalculateSavesSO Result");
            var combatResults = new CombatResults(ModifiedSaves, saveResult);

            DiceResult.RaiseEvent(combatResults.FailedSaves);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;
using WH40K.Stats.Combat;

namespace WH40K.Gameplay.Combat
{
    public class CalculateSaveroles : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
        private int Saves => _gameStats.EnemyUnit.ArmourSave;
        private int Modifier => _gameStats.ActiveUnit.WeaponArmourPen;
        private int ModifiedSaves => Saves - Modifier;

        public CalculateSaveroles(IResult results, GameStatsSO gameStats) : base(results, gameStats) { }

        public CalculateSaveroles()
        {
        }

        public override void Action(List<int> wounds)
        {
            if (wounds == null || wounds.Count == 0) return;
            OnEnable();
            _diceAction.RaiseEvent(wounds);
        }

        public override void Result(List<int> saveResult)
        {
            if (saveResult == null || saveResult.Count == 0) return;
            //if (diceEvent != ShootingSubEvents.Save) return;

            Debug.Log("CalculateSavesSO Result");
            var combatResults = new CombatResults(ModifiedSaves, saveResult);

            _diceResult.RaiseEvent(combatResults.FailedSaves);
        }
    }
}
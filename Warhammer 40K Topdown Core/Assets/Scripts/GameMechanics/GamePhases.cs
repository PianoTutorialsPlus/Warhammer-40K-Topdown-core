using System;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace WH40K.GamePhaseHandling
{
    /// <summary>
    /// This script executes the calls from the interaction manager in the specific state.
    /// When EnableNextPhase is called, it enables to the specific game phase manager responsible for that case.
    /// All other game phase managers will be disabled.
    /// </summary>
    public abstract class GamePhases
    {
        private static bool _initialized = false;

        public abstract GamePhase SubEvents { get; } // gets the active game phase
        //public abstract void ResetPreviousPhase(PhaseManagerBase gamePhaseManager); // clears all dependencies of the game phase
        public abstract void ResetActivePlayerUnits(GameStatsSO gameStats); // clears all dependencies of the game phase

        public bool IsEndOfPlayerTurn() { return SubEvents == GamePhase.MovementPhase; }

        public void EnableNextPhase(PhaseManagerBase gamePhaseManager) // Enables the specific game phase manager
        {
            gamePhaseManager.enabled = true;
        }

        public void ResetPreviousPhase(PhaseManagerBase gamePhaseManager) // clears all dependencies of the game phase
        {
            gamePhaseManager.ClearPhase();
            gamePhaseManager.enabled = false;
        }
        protected void AddUnitPhases(GameStatsSO gameStats)
        {
            if (_initialized != false) return;
            foreach (Unit child in gameStats.ActivePlayer.PlayerUnits)
            {
                child.gameObject.AddComponent<UnitMovementPhase>();
                child.unitMovementPhase = child.GetComponent<UnitMovementPhase>();
                
                child.gameObject.AddComponent<UnitShootingPhase>();
                child.unitShootingPhase = child.GetComponent<UnitShootingPhase>();
            }
            foreach (Unit child in gameStats.EnemyPlayer.PlayerUnits)
            {
                child.gameObject.AddComponent<UnitMovementPhase>();
                child.unitMovementPhase = child.GetComponent<UnitMovementPhase>();

                child.gameObject.AddComponent<UnitShootingPhase>();
                child.unitShootingPhase = child.GetComponent<UnitShootingPhase>();
            }

            _initialized = true;
        }
        protected void ResetUnitPhases(GameStatsSO gameStats)
        {
            gameStats.ActiveUnit = null;
            gameStats.EnemyUnit = null;

            foreach (Unit child in gameStats.ActivePlayer.PlayerUnits)
            {
                child.unitMovementPhase.enabled = false;
                child.unitShootingPhase.enabled = false;
            }
            foreach (Unit child in gameStats.EnemyPlayer.PlayerUnits)
            {
                child.unitMovementPhase.enabled = false;
                child.unitShootingPhase.enabled = false;
            }
        }
    }

    public class MovementPhaseBase : GamePhases
    {
        public MovementPhaseBase() { }
        public override GamePhase SubEvents => GamePhase.MovementPhase;

        public override void ResetActivePlayerUnits(GameStatsSO gameStats)
        {
            AddUnitPhases(gameStats);
            ResetUnitPhases(gameStats);

            foreach (Unit child in gameStats.ActivePlayer.PlayerUnits)
            {
                child.unitMovementPhase.enabled = true;

                //child.ResetData();
                //child.PrepareShootingPhase();
            }
        }
    }

    public class ShootingPhaseBase : GamePhases
    {
        public ShootingPhaseBase() { }
        public override GamePhase SubEvents => GamePhase.ShootingPhase;

        public override void ResetActivePlayerUnits(GameStatsSO gameStats)
        {
            AddUnitPhases(gameStats);
            ResetUnitPhases(gameStats);

            foreach (Unit child in gameStats.EnemyPlayer.PlayerUnits)
            {
                child.unitShootingPhase.enabled = true;

                //child.ResetData();
                //child.PrepareMovementPhase();
            }
        }
    }
}
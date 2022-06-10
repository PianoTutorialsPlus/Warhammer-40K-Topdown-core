using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{
    /// <summary>
    /// This script executes the calls from the interaction manager in the specific state.
    /// When EnableNextPhase is called, it enables to the specific game phase manager responsible for that case.
    /// All other game phase managers will be disabled.
    /// </summary>
    public abstract class GamePhases
    {
        protected GameStatsSO _gameStats;

        public GamePhases(GameStatsSO gameStats)
        {
            Debug.Log(gameStats);
            _gameStats = gameStats;
        }

        protected GamePhases()
        {
            Debug.Log("gehe´t g");
        }

        public abstract GamePhase SubEvents { get; } // gets the active game phase
        //public abstract void ResetPreviousPhase(PhaseManagerBase gamePhaseManager); // clears all dependencies of the game phase
        public abstract void SetActivePlayerUnits(); // clears all dependencies of the game phase

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

        protected void ResetUnitPhases()
        {
            _gameStats.ActiveUnit = null;
            _gameStats.EnemyUnit = null;

            foreach (GameObject child in _gameStats.ActivePlayer.PlayerUnits)
            {
                var unitFacade = child.GetComponent<UnitFacade>();

                unitFacade.UnitMovementPhase.enabled = false;
                unitFacade.UnitShootingPhase.enabled = false;
            }
            foreach (GameObject child in _gameStats.EnemyPlayer.PlayerUnits)
            {
                var unitFacade = child.GetComponent<UnitFacade>();

                unitFacade.UnitMovementPhase.enabled = false;
                unitFacade.UnitShootingPhase.enabled = false;
            }
        }
    }

    public class MovementPhaseBase : GamePhases
    {
        public MovementPhaseBase()
        {
        }

        public MovementPhaseBase(GameStatsSO gameStats) : base(gameStats) { }
        public override GamePhase SubEvents => GamePhase.MovementPhase;

        public override void SetActivePlayerUnits()
        {
            ResetUnitPhases();

            foreach (GameObject child in _gameStats.EnemyPlayer.PlayerUnits)
            {
                var unitFacade = child.GetComponent<UnitFacade>();

                unitFacade.UnitMovementPhase.enabled = true;

                //child.ResetData();
                //child.PrepareShootingPhase();
            }
        }
    }

    public class ShootingPhaseBase : GamePhases
    {
        public ShootingPhaseBase()
        {
        }

        public ShootingPhaseBase(GameStatsSO gameStats) : base(gameStats) { }
        public override GamePhase SubEvents => GamePhase.ShootingPhase;

        public override void SetActivePlayerUnits()
        {
            ResetUnitPhases();

            foreach (GameObject child in _gameStats.ActivePlayer.PlayerUnits)
            {
                var unitFacade = child.GetComponent<UnitFacade>();

                unitFacade.UnitShootingPhase.enabled = true;

                //child.ResetData();
                //child.PrepareMovementPhase();
            }
        }
    }
}
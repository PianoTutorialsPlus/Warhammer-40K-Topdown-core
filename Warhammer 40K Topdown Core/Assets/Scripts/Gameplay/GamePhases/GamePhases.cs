using WH40K.Gameplay.Core;
using WH40K.Gameplay.PlayerEvents;

namespace WH40K.Gameplay.GamePhaseEvents
{
    /// <summary>
    /// This script executes the calls from the interaction manager in the specific state.
    /// When EnableNextPhase is called, it enables to the specific game phase manager responsible for that case.
    /// All other game phase managers will be disabled.
    /// </summary>
    public abstract class GamePhases
    {
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
            GameStats.ActiveUnit = null;
            GameStats.EnemyUnit = null;

            foreach (UnitFacade child in GameStats.ActivePlayer.PlayerUnits)
            {
                child.UnitMovementPhase.enabled = false;
                child.UnitShootingPhase.enabled = false;
            }
            foreach (UnitFacade child in GameStats.EnemyPlayer.PlayerUnits)
            {
                child.UnitMovementPhase.enabled = false;
                child.UnitShootingPhase.enabled = false;
            }
        }
    }

    public class MovementPhaseBase : GamePhases
    {
        public MovementPhaseBase() { }
        public override GamePhase SubEvents => GamePhase.MovementPhase;

        public override void SetActivePlayerUnits()
        {
            ResetUnitPhases();

            foreach (UnitFacade child in GameStats.EnemyPlayer.PlayerUnits)
            {
                child.UnitMovementPhase.enabled = true;

                //child.ResetData();
                //child.PrepareShootingPhase();
            }
        }
    }

    public class ShootingPhaseBase : GamePhases
    {
        public ShootingPhaseBase() { }
        public override GamePhase SubEvents => GamePhase.ShootingPhase;

        public override void SetActivePlayerUnits()
        {
            ResetUnitPhases();

            foreach (UnitFacade child in GameStats.ActivePlayer.PlayerUnits)
            {
                child.UnitShootingPhase.enabled = true;

                //child.ResetData();
                //child.PrepareMovementPhase();
            }
        }
    }
}
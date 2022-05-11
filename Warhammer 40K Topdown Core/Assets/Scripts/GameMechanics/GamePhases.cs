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
        public abstract GamePhase SubEvents { get; } // gets the active game phase
        //public abstract void ResetPreviousPhase(PhaseManagerBase gamePhaseManager); // clears all dependencies of the game phase
        public abstract void ResetActivePlayerUnits(); // clears all dependencies of the game phase

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

            foreach (Unit child in GameStats.ActivePlayer.PlayerUnits)
            {
                child.unitMovementPhase.enabled = false;
                child.unitShootingPhase.enabled = false;
            }
            foreach (Unit child in GameStats.EnemyPlayer.PlayerUnits)
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

        public override void ResetActivePlayerUnits()
        {
            ResetUnitPhases();

            foreach (Unit child in GameStats.ActivePlayer.PlayerUnits)
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

        public override void ResetActivePlayerUnits()
        {
            ResetUnitPhases();

            foreach (Unit child in GameStats.EnemyPlayer.PlayerUnits)
            {
                child.unitShootingPhase.enabled = true;

                //child.ResetData();
                //child.PrepareMovementPhase();
            }
        }
    }
}
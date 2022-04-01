using UnityEngine;
using WH40K.ShootingPhaseHandler;
using WH40K.UnitHandler;

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
        public abstract GamePhase SetNextPhaseToActive(); // sets the next game phase

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
    }

    public class MovementPhaseBase : GamePhases
    {
        public MovementPhaseBase() { }
        public override GamePhase SubEvents => GamePhase.MovementPhase;

        public override GamePhase SetNextPhaseToActive()
        {
            return GamePhase.ShootingPhase;
        }

        //public override void EnableNextPhase(PhaseManagerBase gamePhaseManager)
        //{
        //    gamePhaseManager.enabled = true;
        //    //foreach (PhaseManagerBase phase in gamePhases)
        //    //{
        //    //    if (phase.GetComponent<MovementPhaseManager>() ? phase.enabled = true : phase.enabled = false) { }
        //    //}
        //}

        //public override void ResetPreviousPhase(PhaseManagerBase gamePhaseManager)
        //{
        //    //MovementPhaseManager movementPhase = (MovementPhaseManager)gamePhases[0];
        //    gamePhaseManager.ClearPhase();
        //    gamePhaseManager.enabled = false;
        //}

        public override void ResetActivePlayerUnits(GameStatsSO gameStats)
        {
            gameStats.activeUnit = null;

            foreach (Unit child in gameStats.activePlayer._playerUnits)
            {
                child.gameObject.AddComponent<UnitMovementPhase>();
                child.unitMovementPhase = child.GetComponent<UnitMovementPhase>();
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

        public override GamePhase SetNextPhaseToActive()
        {
            return GamePhase.MovementPhase;
        }

        //public override void EnableNextPhase(PhaseManagerBase gamePhaseManager)
        //{
        //    gamePhaseManager.enabled = true;
        //    //foreach (PhaseManagerBase phase in gamePhases)
        //    //{
        //    //    if (phase.GetComponent<ShootingPhaseManager>() ? phase.enabled = true : phase.enabled = false) { }
        //    //}
        // }

        //public override void ResetPreviousPhase(PhaseManagerBase gamePhaseManager)
        //{
        //    //ShootingPhaseManager shootingPhase = (ShootingPhaseManager)gamePhases[1];  
        //    gamePhaseManager.ClearPhase();
        //    gamePhaseManager.enabled = false;
        //}

        public override void ResetActivePlayerUnits(GameStatsSO gameStats)
        {
            gameStats.activeUnit = null;
            gameStats.enemyUnit = null;

            foreach (Unit child in gameStats.enemyPlayer._playerUnits)
            {
                //var test = new GameObject().AddComponent<UnitMovementPhase>();

                child.gameObject.AddComponent<UnitMovementPhase>();
                child.unitMovementPhase = child.GetComponent<UnitMovementPhase>();
                child.unitMovementPhase.enabled = true;

                //child.ResetData();
                //child.PrepareMovementPhase();
            }
        }
    }
}
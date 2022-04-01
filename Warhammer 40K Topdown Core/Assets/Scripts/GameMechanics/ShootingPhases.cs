using WH40K.UnitHandler;

namespace WH40K.ShootingPhaseHandler
{
    /// <summary>
    /// This script executes the calls from the shooting phase manager in the specific state.
    /// </summary>
    public abstract class ShootingPhases
    {
        public ShootingPhases() { }
        public abstract ShootingPhase SubEvents { get; } // gets the active subphase
        public abstract ShootingPhase SetPhase(); // sets the next subphase
        public virtual bool HandlePhase(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; } // handles the selection subphase
        public virtual bool HandlePhase() { return false; } // handles the shooting subphase
        public virtual bool Next(GameStatsSO gameStats) { return false; } // disables the current unit for this game phase
    }

    public class S_Selection : ShootingPhases
    {
        public S_Selection() { }
        public override ShootingPhase SubEvents => ShootingPhase.Selection;
        public override ShootingPhase SetPhase() { return ShootingPhase.Shoot; }
        public override bool HandlePhase(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents)
        {
            foreach (Unit child in gameStats.activePlayer._playerUnits)
            {
                if (child.done)
                {
                    _battleroundEvents.FillMethods(child, false, true, false, false);
                    continue;
                }

                if (child == gameStats.activeUnit)
                {
                    _battleroundEvents.FillMethods(child, true, true, true, true);
                }
                else
                {
                    _battleroundEvents.FillMethods(child, false, true, true, true);
                }
            }

            foreach (Unit child in gameStats.enemyPlayer._playerUnits) _battleroundEvents.FillMethods(child, false, true, true, false);

            return gameStats.activeUnit != null ? true : false;
        }
    }

    public class S_Shoot : ShootingPhases
    {
        public S_Shoot() { }
        public override ShootingPhase SubEvents => ShootingPhase.Shoot;
        public override ShootingPhase SetPhase() { return ShootingPhase.Next; }
        public override bool HandlePhase() { return true; }

    }

    public class S_Next : ShootingPhases
    {
        public S_Next() { }
        public override ShootingPhase SubEvents => ShootingPhase.Next;
        public override ShootingPhase SetPhase() { return ShootingPhase.Selection; }
        public override bool Next(GameStatsSO gameStats)
        {
            gameStats.activeUnit.Freeze();
            gameStats.activeUnit = null;
            return true;
        }
    }
}
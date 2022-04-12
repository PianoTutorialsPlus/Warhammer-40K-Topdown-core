using WH40K.Essentials;
using WH40K.UI;

namespace WH40K.GameMechanics
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
            foreach (Unit child in gameStats.ActivePlayer.PlayerUnits)
            {
                _battleroundEvents.FillMethods(child);
            }

            foreach (Unit child in gameStats.EnemyPlayer.PlayerUnits) _battleroundEvents.FillMethods(child);

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
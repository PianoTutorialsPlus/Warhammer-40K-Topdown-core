using WH40K.Essentials;
using WH40K.UI;

namespace WH40K.GameMechanics
{
    /// <summary>
    /// This script executes the calls from the shooting phase manager in the specific state.
    /// </summary>
    public abstract class ShootingPhases
    {
        private IGamePhase _gamePhase;
        protected GameStatsSO _gameStats => _gamePhase.GameStats;
        protected IPhase _phase => _gamePhase.BattleroundEvents;

        public ShootingPhases(IGamePhase gamePhase) 
        {
            _gamePhase = gamePhase;
        }

        public abstract ShootingPhase SubEvents { get; } // gets the active subphase
        public abstract ShootingPhase SetPhase(); // sets the next subphase
        public virtual bool HandlePhase(GameStatsSO gameStats) { return false; } // handles the selection subphase
        public virtual bool HandlePhase() { return false; } // handles the shooting subphase
        public virtual bool Next(GameStatsSO gameStats) { return false; } // disables the current unit for this game phase
        public virtual void ClearPhase(GameStatsSO gamesStats)
        {
            //Debug.Log("Clear");
            _phase.ClearPhase(gamesStats);
        }
    }

    public class S_Selection : ShootingPhases
    {
        public S_Selection(IGamePhase gamePhase) : base(gamePhase) { }
        public override ShootingPhase SubEvents => ShootingPhase.Selection;
        public override ShootingPhase SetPhase() { return ShootingPhase.Shoot; }
        public override bool HandlePhase(GameStatsSO gameStats)
        {
            _phase.HandlePhase(gameStats);
            //_gameStats.ActiveUnit.Activate();
            return gameStats.activeUnit != null ? true : false;
        }
    }

    public class S_Shoot : ShootingPhases
    {
        public S_Shoot(IGamePhase gamePhase) : base(gamePhase) { }
        public override ShootingPhase SubEvents => ShootingPhase.Shoot;
        public override ShootingPhase SetPhase() { return ShootingPhase.Next; }
        public override bool HandlePhase() { return true; }

    }

    public class S_Next : ShootingPhases
    {
        public S_Next(IGamePhase gamePhase) : base(gamePhase) { }
        public override ShootingPhase SubEvents => ShootingPhase.Next;
        public override ShootingPhase SetPhase() { return ShootingPhase.Selection; }
        public override bool Next(GameStatsSO gameStats)
        {
            gameStats.activeUnit.Freeze();
            _gameStats.ActiveUnit = null;
            return true;
        }
    }
}
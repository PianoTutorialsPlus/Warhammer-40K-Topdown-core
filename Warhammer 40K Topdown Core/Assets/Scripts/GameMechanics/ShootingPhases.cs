﻿using WH40K.Essentials;

namespace WH40K.GameMechanics
{
    /// <summary>
    /// This script executes the calls from the shooting phase manager in the specific state.
    /// </summary>
    public abstract class ShootingPhases
    {
        private IGamePhase _gamePhase;
        protected IPhase _phase => _gamePhase.BattleroundEvents;

        public ShootingPhases(IGamePhase gamePhase)
        {
            _gamePhase = gamePhase;
        }

        public abstract ShootingPhase SubEvents { get; } // gets the active subphase
        public virtual void HandlePhase() { } // handles the selection subphase
        public virtual bool Next() { return false; } // disables the current unit for this game phase
        public virtual void ClearPhase()
        {
            _phase.ClearPhase();
        }
    }

    public class S_Selection : ShootingPhases
    {
        public S_Selection(IGamePhase gamePhase) : base(gamePhase) { }
        public override ShootingPhase SubEvents => ShootingPhase.Selection;
        public override void HandlePhase()
        {
            _phase.HandlePhase();
            //_gameStats.ActiveUnit.Activate();
        }
    }

    public class S_Shoot : ShootingPhases
    {
        public S_Shoot(IGamePhase gamePhase) : base(gamePhase) { }
        public override ShootingPhase SubEvents => ShootingPhase.Shoot;
        public override void HandlePhase() { }
    }

    public class S_Next : ShootingPhases
    {
        public S_Next(IGamePhase gamePhase) : base(gamePhase) { }
        public override ShootingPhase SubEvents => ShootingPhase.Next;
        public override bool Next()
        {
            GameStats.ActiveUnit.Freeze();
            GameStats.ActiveUnit = null;
            return true;
        }
    }
}
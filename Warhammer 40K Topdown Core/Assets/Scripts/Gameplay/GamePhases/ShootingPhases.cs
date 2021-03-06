using System;
using UnityEngine;
using WH40K.Gameplay.Events;
using WH40K.Stats;
using Zenject;

namespace WH40K.Gameplay.GamePhaseEvents
{
    /// <summary>
    /// This script executes the calls from the shooting phase manager in the specific state.
    /// </summary>
    public abstract class ShootingPhases : PhasesBase
    {
        [Inject] protected GameStatsSO _gameStats;
        [Inject] protected IPhase _phase;/* => _gamePhase.BattleroundEvents;*/

        protected ShootingPhases() { }

        public virtual void HandlePhase() { } // handles the selection subphase
        public virtual bool Next() { return false; } // disables the current unit for this game phase
        public virtual void ClearPhase()
        {
            _phase.ClearPhase();
        }
    }

    public class S_Selection : ShootingPhases
    {
        public S_Selection() { }
        public override Enum SubEvents => ShootingPhase.Selection;
        public override void HandlePhase()
        {
            _phase.HandlePhase();
            //_gameStats.ActiveUnit.Activate();
        }
    }

    public class S_Shoot : ShootingPhases
    {
        public S_Shoot() { }
        public override Enum SubEvents => ShootingPhase.Shoot;
        public override void HandlePhase() { }
    }

    public class S_Next : ShootingPhases
    {
        public S_Next() { }
        public override Enum SubEvents => ShootingPhase.Next;
        public override bool Next()
        {
            _gameStats.ActiveUnit.Freeze();
            _gameStats.ActiveUnit = null;
            return true;
        }
    }
}
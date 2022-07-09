using System;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Gameplay.Core;
using WH40K.Gameplay.Events;
using WH40K.Stats;
using Zenject;

namespace WH40K.Gameplay.GamePhaseEvents
{
    /// <summary>
    /// This script executes the calls from the movement phase manager in the specific state.
    /// </summary>
    public abstract class MovementPhases : PhasesBase
    {
        [Inject] protected GameStatsSO _gameStats;
        [Inject] protected IPhase _phase;
        protected GameTable _gameTable => _gameStats.GameTable.GameTable.GetComponent<GameTable>();

        protected MovementPhases() { }

        public virtual void HandlePhase() { } // handles the selection subphase
        public virtual bool Next() { return false; } // disables the current unit for this game phase

        public virtual void ClearPhase()
        {
            //Debug.Log("Clear");
            _phase.ClearPhase();
        }
    }

    public class M_Selection : MovementPhases
    {
        public M_Selection() { }

        public override Enum SubEvents => MovementPhase.Selection;
        public override void HandlePhase()
        {
            Debug.Log("MPhase : " + _gameStats);
            _phase.HandlePhase();
        }
    }

    public class Move : MovementPhases
    {
        public Move() { }

        public override Enum SubEvents => MovementPhase.Move;

        public override void HandlePhase()
        {
            _phase.HandlePhase();
            if (_gameStats.ActiveUnit != null) _gameStats.ActiveUnit.Activate();
            _gameTable.onTapDownAction += MoveUnit;
        }

        public override bool Next()
        {
            if (_gameStats.ActiveUnit != null) return _gameStats.ActiveUnit.IsDone;
            return false;
        }
        public override void ClearPhase()
        {
            _phase.ClearPhase();
            _gameTable.onTapDownAction -= MoveUnit;
        }
        public void MoveUnit(Vector3 position)
        {
            _gameStats.ActiveUnit.SetDestination(position);
        }
    }

    public class MNext : MovementPhases
    {
        public MNext() { }

        //public MNext(GameStatsSO gameStats, IPhase gamePhase) : base(gameStats, gamePhase) { }
        public override Enum SubEvents => MovementPhase.Next;

        public override bool Next()
        {
            //gameStats.activeUnit.Freeze();
            _gameStats.ActiveUnit = null;
            return true;
        }
    }
}

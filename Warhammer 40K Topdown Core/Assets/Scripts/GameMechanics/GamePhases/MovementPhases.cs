﻿using UnityEngine;
using WH40K.Core;
using WH40K.Events;

namespace WH40K.GamePhaseEvents
{
    /// <summary>
    /// This script executes the calls from the movement phase manager in the specific state.
    /// </summary>
    public abstract class MovementPhases
    {
        protected IPhase _phase;
        protected GameTable _gameTable => GameStats.GameTable.gameTable;

        public MovementPhases(IPhase gamePhase)
        {
            _phase = gamePhase;
        }

        public abstract MovementPhase SubEvents { get; } // gets the active subphase
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
        public M_Selection(IPhase gamePhase) : base(gamePhase) { }

        public override MovementPhase SubEvents => MovementPhase.Selection;
        public override void HandlePhase()
        {
            _phase.HandlePhase();
        }
    }

    public class Move : MovementPhases
    {
        public Move(IPhase gamePhase) : base(gamePhase) { }

        public override MovementPhase SubEvents => MovementPhase.Move;

        public override void HandlePhase()
        {
            _phase.HandlePhase();
            if(GameStats.ActiveUnit != null)GameStats.ActiveUnit.Activate();
            _gameTable.onTapDownAction += MoveUnit;
        }

        public override bool Next()
        {
            if (GameStats.ActiveUnit != null) return GameStats.ActiveUnit.IsDone;
            return false;
        }
        public override void ClearPhase()
        {
            _phase.ClearPhase();
            _gameTable.onTapDownAction -= MoveUnit;
        }
        public void MoveUnit(Vector3 position)
        {
            GameStats.ActiveUnit.SetDestination(position);
        }
    }

    public class MNext : MovementPhases
    {
        public MNext(IPhase gamePhase) : base(gamePhase) { }
        public override MovementPhase SubEvents => MovementPhase.Next;

        public override bool Next()
        {
            //gameStats.activeUnit.Freeze();
            GameStats.ActiveUnit = null;
            return true;
        }
    }
}

using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics
{
    /// <summary>
    /// This script executes the calls from the movement phase manager in the specific state.
    /// </summary>
    public abstract class MovementPhases
    {
        private IGamePhase _gamePhase;
        protected IPhase _phase => _gamePhase.BattleroundEvents;

        public MovementPhases(IGamePhase gamePhase)
        {
            _gamePhase = gamePhase;
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
        public M_Selection(IGamePhase gamePhase) : base(gamePhase) { }

        public override MovementPhase SubEvents => MovementPhase.Selection;
        public override void HandlePhase()
        {
            _phase.HandlePhase();
        }
    }

    public class Move : MovementPhases
    {
        public Move(IGamePhase gamePhase) : base(gamePhase) { }

        public override MovementPhase SubEvents => MovementPhase.Move;

        public override void HandlePhase()
        {
            _phase.HandlePhase();
            GameStats.ActiveUnit.Activate();
            GameStats.GameTable.gameTable.OnTapDownAction += MoveUnit;
        }

        public override bool Next()
        {
            return GameStats.ActiveUnit.IsDone;
        }
        public override void ClearPhase()
        {
            _phase.ClearPhase();
            GameStats.GameTable.gameTable.OnTapDownAction -= MoveUnit;
        }
        public void MoveUnit(Vector3 position)
        {
            GameStats.ActiveUnit.SetDestination(position);
        }
    }

    public class MNext : MovementPhases
    {
        public MNext(IGamePhase gamePhase) : base(gamePhase) { }
        public override MovementPhase SubEvents => MovementPhase.Next;

        public override bool Next()
        {
            //gameStats.activeUnit.Freeze();
            GameStats.ActiveUnit = null;
            return true;
        }
    }
}

using System;
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
        public GameStatsSO _gameStats => _gamePhase.GameStats;
        public IPhase _phase => _gamePhase.BattleroundEvents;

        public MovementPhases(IGamePhase gamePhase)
        {
            _gamePhase = gamePhase;
        }

        public abstract MovementPhase SubEvents { get; } // gets the active subphase
        //public abstract MovementPhase SetNextPhase(); // sets the next subphase
        public virtual void HandlePhase(GameStatsSO gameStats) { } // handles the selection subphase
                                                                                 //public virtual bool HandleMove(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; } // handles the movement subphase
        public virtual bool Next(GameStatsSO gameStats) { return false; } // disables the current unit for this game phase

        public virtual void ClearPhase(GameStatsSO gamesStats)
        {
            Debug.Log("Clear");
            _phase.ClearPhase(gamesStats);
        }
    }

    public class M_Selection : MovementPhases
    {
        public M_Selection(IGamePhase gamePhase) : base(gamePhase) { }

        public override MovementPhase SubEvents => MovementPhase.Selection;
        //public override MovementPhase SetNextPhase() {  return MovementPhase.Move; }
        public override void HandlePhase(GameStatsSO gameStats)
        {
            _phase.HandlePhase(gameStats);
        }
    }

    public class Move : MovementPhases
    {
        public Move(IGamePhase gamePhase) : base(gamePhase) { }

        public override MovementPhase SubEvents => MovementPhase.Move;
        //public override MovementPhase SetNextPhase() { return MovementPhase.Next; }

        public override void HandlePhase(GameStatsSO gameStats)
        {
            _phase.HandlePhase(gameStats);
            _gameStats.ActiveUnit.Activate();
            _gameStats.GameTable.gameTable.OnTapDownAction += MoveUnit;
        }

        public override bool Next(GameStatsSO gameStats)
        {
            return gameStats.ActiveUnit.IsDone;
        }
        public override void ClearPhase(GameStatsSO gameStats)
        {
            _phase.ClearPhase(gameStats);
            _gameStats.GameTable.gameTable.OnTapDownAction -= MoveUnit;
        }
        public void MoveUnit(Vector3 position)
        {
            _gameStats.ActiveUnit.SetDestination(position);
        }
    }

    public class MNext : MovementPhases
    {
        public MNext(IGamePhase gamePhase) : base(gamePhase) { }
        public override MovementPhase SubEvents => MovementPhase.Next;
        //public override MovementPhase SetNextPhase() { return MovementPhase.Selection; }

        public override bool Next(GameStatsSO gameStats)
        {
            //gameStats.activeUnit.Freeze();
            _gameStats.ActiveUnit = null;
            return true;
        }

    }
}




//using System;
///// <summary>
///// This script executes the calls from the movement phase manager in the specific state.
///// </summary>
//public abstract class MovementPhases
//{
//    public IPhase _phase;
//    public MovementPhases(IPhase phase)
//    {
//        _phase = phase;
//    }

//    public abstract MovementPhase SubEvents { get; } // gets the active subphase
//    public abstract MovementPhase SetPhase(); // sets the next subphase
//    public virtual bool HandlePhase(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; } // handles the selection subphase
//    public virtual bool HandleMove(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; } // handles the movement subphase
//    public virtual bool Next(GameStatsSO gameStats) { return false; } // disables the current unit for this game phase

//}

//public class M_Selection : MovementPhases
//{
//    public M_Selection(IPhase phase) : base(phase) { }

//    public override MovementPhase SubEvents => MovementPhase.Selection;
//    public override MovementPhase SetPhase() { return MovementPhase.Move; }
//    public override bool HandlePhase(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents)
//    {
//        _phase.HandlePhase(gameStats);
//        //_battleroundEvents.HandlePhase(gameStats);
//        return gameStats.activeUnit != null ? true : false;
//    }

//}

//public class Move : MovementPhases
//{
//    public Move(IPhase phase) : base(phase) { }
//    public override MovementPhase SubEvents => MovementPhase.Move;
//    public override MovementPhase SetPhase() { return MovementPhase.Next; }

//    public override bool HandleMove(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents)
//    {
//        _phase.HandleMove(gameStats);
//        //_battleroundEvents.FillMethods(gameStats.activeUnit, true, true, true, false);
//        gameStats.activeUnit.activated = true;
//        return true;
//    }

//    public override bool Next(GameStatsSO gameStats)
//    {
//        return gameStats.activeUnit.done;
//    }
//}

//public class MNext : MovementPhases
//{
//    public MNext(IPhase phase) : base(phase) { }
//    public override MovementPhase SubEvents => MovementPhase.Next;
//    public override MovementPhase SetPhase() { return MovementPhase.Selection; }

//    public override bool Next(GameStatsSO gameStats)
//    {
//        //gameStats.activeUnit.Freeze();
//        gameStats.activeUnit = null;
//        return true;
//    }

//}
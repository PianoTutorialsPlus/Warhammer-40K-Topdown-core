

/// <summary>
/// This script executes the calls from the movement phase manager in the specific state.
/// </summary>

public abstract class MovementPhases
{
    public MovementPhases() { }
    public abstract MovementPhase SubEvents { get; } // gets the active subphase
    public abstract MovementPhase SetPhase(); // sets the next subphase
    public virtual bool HandlePhase(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; } // handles the selection subphase
    public virtual bool HandleMove(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; } // handles the movement subphase
    public virtual bool Next(GameStatsSO gameStats) { return false; } // disables the current unit for this game phase

}

public class M_Selection : MovementPhases
{
    public M_Selection() { }
    public override MovementPhase SubEvents => MovementPhase.Selection;
    public override MovementPhase SetPhase() { return MovementPhase.Move; }
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

public class Move : MovementPhases
{
    public Move() { }
    public override MovementPhase SubEvents => MovementPhase.Move;
    public override MovementPhase SetPhase() { return MovementPhase.Next; }

    public override bool HandleMove(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents)
    {
        _battleroundEvents.FillMethods(gameStats.activeUnit, true, true, true, false);
        gameStats.activeUnit.activated = true;
        return true;
    }

    public override bool Next(GameStatsSO gameStats)
    {
        return gameStats.activeUnit.done;
    }
}

public class MNext : MovementPhases
{
    public MNext() { }
    public override MovementPhase SubEvents => MovementPhase.Next;
    public override MovementPhase SetPhase() { return MovementPhase.Selection; }

    public override bool Next(GameStatsSO gameStats)
    {
        //gameStats.activeUnit.Freeze();
        gameStats.activeUnit = null;
        return true;
    }

}
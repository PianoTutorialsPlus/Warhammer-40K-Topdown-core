using System;

public abstract class MovementPhases
{
    public abstract MovementPhase SubEvents { get; }
    public abstract MovementPhase SetPhase();
    public virtual bool HandlePhase(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; }
    public virtual bool HandleMove(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; }
    
}

public class MSelection: MovementPhases
{
    public MSelection() { }
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
                //Debug.Log("Element");
            }

            else
            {
                //Debug.Log("Element");
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
    public override MovementPhase SetPhase() { return MovementPhase.None; }
    public override bool HandleMove(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents)
    {
        _battleroundEvents.FillMethods(gameStats.activeUnit, true, true, true, false);
        //Debug.Log("Move");
        gameStats.activeUnit.activated = true;
        return true;
    }
    
}
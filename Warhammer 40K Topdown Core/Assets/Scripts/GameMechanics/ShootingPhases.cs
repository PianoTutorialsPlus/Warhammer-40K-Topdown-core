public abstract class ShootingPhases
{
    public ShootingPhases() { }
    public abstract ShootingPhase SubEvents { get; }
    public abstract ShootingPhase SetPhase();
    public virtual bool HandlePhase(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents) { return false; }
    public virtual bool HandlePhase() { return false; }
    
}

public class Selection: ShootingPhases
{
    public Selection() { }
    public override ShootingPhase SubEvents => ShootingPhase.Selection;
    public override ShootingPhase SetPhase() { return ShootingPhase.Shoot; }
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
                //Debug.Log("Element");
                _battleroundEvents.FillMethods(child, false, true, true, true);
            }
        }
        foreach (Unit child in gameStats.enemyPlayer._playerUnits) _battleroundEvents.FillMethods(child, false, true, true, false);

        return gameStats.activeUnit != null ? true: false;
    }   
}

public class Shoot: ShootingPhases
{
    public Shoot() { }
    public override ShootingPhase SubEvents => ShootingPhase.Shoot;
    public override bool HandlePhase() { return true; }
    public override ShootingPhase SetPhase()
    {
        return ShootingPhase.None;
    }

}
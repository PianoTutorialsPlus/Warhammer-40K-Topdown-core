using System.Collections.Generic;

/// <summary>
/// This script executes the calls from the shooting phase manager in the specific state.
/// </summary>

public abstract class ShootingSubPhases
{
    public ShootingSubPhases() { }
    public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
    public abstract ShootingSubEvents SetSubPhase(); // sets the next subphase
    public abstract ICalculation SetCalculation(List<RollTheDiceSO> calculations); // refers to the active calculation process
    public abstract void HandleShooting(List<int> parameter, ICalculation calculateHits, GameStatsSO gameStats); // handles the subphases
    public virtual List<int> ProcessResult(List<int> result) { return result; } // placeholder for results, may be removed
    public virtual List<int> GetResult(int equalizer, List<int> result) { return null; } // post process of calculation
}

public class SelectEnemy : ShootingSubPhases
{
    ICalculation calculation;
    public SelectEnemy() { }
    public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Hit; }
    public override ICalculation SetCalculation(List<RollTheDiceSO> calculations)// { return (ICalculation)calculations[0]; }
    {
        if (calculation == null) calculation = new SelectEnemiesSO(calculations);
        return calculation;
    }

    public override void HandleShooting(List<int> parameter, ICalculation selectEnemy, GameStatsSO gameStats)
    {
        selectEnemy.Action(gameStats);
    }

}

public class Hit : ShootingSubPhases
{
    public Hit() { }
    ICalculation calculation;

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Wound; }
    public override ICalculation SetCalculation(List<RollTheDiceSO> calculations) //{ return (ICalculation)calculations[1]; }
    {
        if (calculation == null) calculation = new CalculateHitsSO(calculations);
        return calculation;
    }

    public override void HandleShooting(List<int> parameter, ICalculation calculateHits, GameStatsSO gameStats)
    {
        calculateHits.Action(gameStats);
    }

    public override List<int> GetResult(int toHit, List<int> hitResult)
    {
        List<int> hits = new List<int>();

        foreach (int result in hitResult)
        {
            //Debug.Log("Hit result: " + result);
            if (result >= toHit)
                hits.Add(result);
        }
        return hits;
    }
}

public class Wound : ShootingSubPhases
{
    public Wound() { }
    ICalculation calculation;

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Save; }

    public override ICalculation SetCalculation(List<RollTheDiceSO> calculations) //{ return (ICalculation)calculations[2]; }
    {
        if (calculation == null) calculation = new CalculateWoundsSO(calculations);
        return calculation;
    }

    public override void HandleShooting(List<int> parameter, ICalculation calculateWounds, GameStatsSO gameStats)
    {
        calculateWounds.Action(parameter, gameStats);
    }

    public override List<int> GetResult(int toWound, List<int> woundResult)
    {
        List<int> wounds = new List<int>();

        foreach (int result in woundResult)
        {
            //Debug.Log("Wounds: " + result);
            if (result >= toWound)
            {
                //Debug.Log("toWounds: " + toWound);
                wounds.Add(result);
            }
        }
        return wounds;
    }
}

public class Save : ShootingSubPhases
{
    public Save() { }
    ICalculation calculation;

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Damage; }
    public override ICalculation SetCalculation(List<RollTheDiceSO> calculations)// { return (ICalculation)calculations[3]; }
    {
        if (calculation == null) calculation = new CalculateSaverolesSO(calculations);
        return calculation;
    }

    public override void HandleShooting(List<int> parameter, ICalculation calculateSaves, GameStatsSO gameStats)
    {
        calculateSaves.Action(parameter, gameStats);
    }

    public override List<int> GetResult(int saves, List<int> saveResult)
    {
        List<int> notSaved = new List<int>();

        foreach (int result in saveResult)
        {
            //Debug.Log("saveResult: " + result);
            if (result < (saves))
                notSaved.Add(result);
        }
        return notSaved;
    }
}
public class Damage : ShootingSubPhases
{
    public Damage() { }
    ICalculation calculation;

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.SelectEnemy; }
    public override ICalculation SetCalculation(List<RollTheDiceSO> calculations)// { return (ICalculation)calculations[4]; }
    {
        if (calculation == null) calculation = new DealDamageSO(calculations);
        return calculation;
    }

    public override void HandleShooting(List<int> parameter, ICalculation dealDamage, GameStatsSO gameStats)
    {
        dealDamage.Action(parameter, gameStats);
    }
}
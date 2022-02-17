using System.Collections.Generic;
//using UnityEngine;

/// <summary>
/// This script executes the calls from the shooting phase manager in the specific state.
/// </summary>

public abstract class ShootingSubPhases
{
    public ShootingSubPhases() { }
    public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
    public abstract ShootingSubEvents SetSubPhase(); // sets the next subphase
    public abstract CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations); // refers to the active calculation process
    public abstract void HandleShooting(List<int> parameter, CalculationBaseSO calculateHits, GameStatsSO gameStats); // handles the subphases
    public virtual List<int> ProcessResult(List<int> result) { return result; } // placeholder for results, may be removed
    public virtual List<int> GetResult(int equalizer, List<int> result) { return null; } // post process of calculation
}

public class SelectEnemy : ShootingSubPhases
{
    public SelectEnemy() { }
    public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Hit; }
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[0]; }

    public override void HandleShooting(List<int> parameter, CalculationBaseSO selectEnemy, GameStatsSO gameStats)
    {
        selectEnemy.Action(gameStats);
    }

}

public class Hit : ShootingSubPhases
{
    public Hit() { }

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Wound; }
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[1]; }

    public override void HandleShooting(List<int> parameter, CalculationBaseSO calculateHits, GameStatsSO gameStats)
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

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Save; }

    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[2]; }

    public override void HandleShooting(List<int> parameter, CalculationBaseSO calculateWounds, GameStatsSO gameStats)
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

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Damage; }
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[3]; }

    public override void HandleShooting(List<int> parameter, CalculationBaseSO calculateSaves, GameStatsSO gameStats)
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

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;
    public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.SelectEnemy; }
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[4]; }

    public override void HandleShooting(List<int> parameter, CalculationBaseSO dealDamage, GameStatsSO gameStats)
    {
        dealDamage.Action(parameter, gameStats);
    }
}
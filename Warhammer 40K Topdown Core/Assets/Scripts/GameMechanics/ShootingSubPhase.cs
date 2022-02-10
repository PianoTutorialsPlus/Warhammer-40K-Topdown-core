using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingSubPhase
{
    public ShootingSubPhase() { }
    public abstract ShootingSubEvents SubEvents { get; }
    public abstract ShootingSubEvents SetSubPhase();
    public abstract CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations);
    public abstract void HandleShooting(List<int> parameter, CalculationBaseSO calculateHits, GameStatsSO gameStats);
    public virtual List<int> ProcessResult(List<int> result) { return result; }
    public virtual List<int> GetResult(int equalizer, List<int> result) { return null; }
}

public class SelectEnemy : ShootingSubPhase
{
    public SelectEnemy() { }

    public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[0]; }
    public override void HandleShooting(List<int> parameter, CalculationBaseSO selectEnemy, GameStatsSO gameStats)
    {
        selectEnemy.Action(gameStats);
    }
    //public override List<int> ProcessResult(List<int> result)
    //{
    //    return result;
    //}
    public override ShootingSubEvents SetSubPhase()
    {
        return ShootingSubEvents.Hit;
    }
    //public override List<int> GetResult(int toHit, List<int> result)
    //{
    //    return null;
    //}
}
public class Hit : ShootingSubPhase
{
    public Hit() { }

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[1]; }
    public override void HandleShooting(List<int> parameter, CalculationBaseSO calculateHits, GameStatsSO gameStats)
    {
        calculateHits.Action(gameStats);
    }
    public override ShootingSubEvents SetSubPhase()
    {
        return ShootingSubEvents.Wound;
    }
    public override List<int> GetResult(int toHit, List<int> hitResult)
    {
        List<int> hits = new List<int>();

        foreach (int result in hitResult)
        {
            Debug.Log("Hit result: " + result);
            if (result >= toHit)
                hits.Add(result);
        }
        return hits;
    }
}

public class Wound : ShootingSubPhase
{
    public Wound() { }

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[2]; }
    public override void HandleShooting(List<int> parameter, CalculationBaseSO calculateWounds, GameStatsSO gameStats)
    {
        calculateWounds.Action(parameter,gameStats);
    }
    public override ShootingSubEvents SetSubPhase()
    {
        return ShootingSubEvents.Save;
    }

    public override List<int> GetResult(int toWound, List<int> woundResult)
    {
        List<int> wounds = new List<int>();

        foreach (int result in woundResult)
        {
            Debug.Log("Wounds: " + result);
            if (result >= toWound)
            {
                Debug.Log("toWounds: " + toWound);
                wounds.Add(result);
            }
        }
        return wounds;
    }
}

public class Save : ShootingSubPhase
{
    public Save() { }

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) {return calculations[3]; }
    public override void HandleShooting(List<int> parameter, CalculationBaseSO calculateSaves, GameStatsSO gameStats)
    {
        calculateSaves.Action(parameter,gameStats);
    }
    public override ShootingSubEvents SetSubPhase()
    {
        return ShootingSubEvents.Damage;
    }
    public override List<int> GetResult(int saves, List<int> saveResult)
    {
        List<int> notSaved = new List<int>();

        foreach (int result in saveResult)
        {
            Debug.Log("saveResult: " + result);
            if (result < (saves))
                notSaved.Add(result);
        }
        return notSaved;
    }
}
public class Damage : ShootingSubPhase
{
    public Damage() { }

    public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;
    public override CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations) { return calculations[4]; }
    public override void HandleShooting(List<int> parameter, CalculationBaseSO dealDamage, GameStatsSO gameStats)
    {
        dealDamage.Action(parameter,gameStats);
    }
    public override ShootingSubEvents SetSubPhase()
    {
        return ShootingSubEvents.none;
    }
    //public override List<int> GetResult(int toHit, List<int> result)
    //{
    //    return null;
    //}
}
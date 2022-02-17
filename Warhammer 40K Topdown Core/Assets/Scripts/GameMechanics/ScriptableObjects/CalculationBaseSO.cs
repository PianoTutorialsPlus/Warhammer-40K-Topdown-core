using System.Collections.Generic;
using UnityEngine;

public abstract class CalculationBaseSO : ScriptableObject
{
    public RollTheDiceSO rollDices;
    public RollTheDiceSO rollSubResult;
    public RollTheDiceSO rollDiceResult;

    public virtual void Action(List<int> action, GameStatsSO gameStats) { }
    public virtual void Action(GameStatsSO gameStats) { }
    public abstract void Result(ShootingSubEvents diceEvent, List<int> Result);
}
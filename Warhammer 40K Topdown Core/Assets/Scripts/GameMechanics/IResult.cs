using System.Collections.Generic;

public interface IResult
{
    RollTheDiceSO DiceAction { get; }
    RollTheDiceSO DiceSubResult { get; }
    RollTheDiceSO DiceResult { get; }
    GameStatsSO GameStats { get; }
}
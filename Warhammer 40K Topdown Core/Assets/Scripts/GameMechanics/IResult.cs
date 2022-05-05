using System.Collections.Generic;
using WH40K.Essentials;

namespace WH40K.GameMechanics
{
    public interface IResult
    {
        RollTheDiceSO DiceAction { get; }
        RollTheDiceSO DiceSubResult { get; }
        RollTheDiceSO DiceResult { get; }
        GameStatsSO GameStats { get; set; }
    }
}
using System.Collections.Generic;

public interface ICalculation
{
    void Action(List<int> action, GameStatsSO gameStats);
    void Action(GameStatsSO gameStats);
    void Result(ShootingSubEvents diceEvent, List<int> Result);
}
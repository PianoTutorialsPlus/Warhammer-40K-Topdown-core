using System.Collections.Generic;

namespace WH40K.GameMechanics.Combat
{
    public interface ICalculation
    {
        void Action(List<int> action = null);
        void Result(ShootingSubEvents diceEvent, List<int> result);
    }
}
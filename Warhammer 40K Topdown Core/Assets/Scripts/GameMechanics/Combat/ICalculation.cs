using System.Collections.Generic;

namespace WH40K.Combat
{
    public interface ICalculation
    {
        void Action(List<int> action = null);
        void Result(ShootingSubEvents diceEvent, List<int> result);
    }
}
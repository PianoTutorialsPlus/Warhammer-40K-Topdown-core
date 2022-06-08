using System.Collections.Generic;

namespace WH40K.Gameplay.Combat
{
    public interface ICalculation
    {
        void Action(List<int> action = null);
        void Result(List<int> result);
    }
}
using System;

namespace WH40K.GameMechanics.Combat
{
    public class WoundTable
    {
        public WoundTable()
        {
        }
        public int ToWound(int strength, int toughness)
        {
            if (strength < 1) throw new ArgumentOutOfRangeException("Strength");
            if (toughness < 1) throw new ArgumentOutOfRangeException("Toughness");

            return strength >= 2 * toughness
                ? 2
                : strength > toughness
                    ? 3
                    : strength == toughness
                        ? 4
                        : 2 * strength <= toughness
                            ? 6
                            : 5;
        }

    }
}
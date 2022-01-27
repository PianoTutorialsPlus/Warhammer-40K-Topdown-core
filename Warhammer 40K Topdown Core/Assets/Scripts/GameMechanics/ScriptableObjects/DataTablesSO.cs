using UnityEngine;

[CreateAssetMenu(fileName = "Data Tables", menuName = "Game/Data Tables")]
public class DataTablesSO : ScriptableObject
{
    public int WoundTable(int strength, int toughness)
    {
        int toWound = 0;
        if (strength >= 2 * toughness)
        {
            toWound = 2;
        }
        else if (strength > toughness)
        {
            toWound = 3;
        }
        else if (strength == toughness)
        {
            toWound = 4;
        }
        else if (2 * strength < toughness)
        {
            toWound = 6;
        }
        else if (strength < toughness)
        {
            toWound = 5;
        }

        return toWound;
    }

}

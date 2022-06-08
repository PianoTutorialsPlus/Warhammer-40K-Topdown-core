using UnityEngine;
using WH40K.Stats.Player;

namespace WH40K.Armies.Necrons
{
    [CreateAssetMenu(fileName = "Tesla Carabine", menuName = "Weapons/Tesla Carabine")]
    public class TeslaCarbineSO : WeaponSO // INHARITANCE
    {
        public override int HitModifier(int hits) // POLYMORPHISM
        {
            hits += 2;
            return hits;
        }
    }
}
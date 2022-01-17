using UnityEngine;


[CreateAssetMenu(fileName = "Tesla Carabine", menuName = "Weapons/Tesla Carabine")]
public class TeslaCarabineSO : WeaponSO // INHARITANCE
{
    public override int HitModifier(int hits) // POLYMORPHISM
    {
        hits += 2;
        return hits;
    }
}



namespace WH40K.Stats.Player
{
    public interface IWeaponStats
    {
        string WeaponName { get; }
        int WeaponRange { get; }
        int WeaponShots { get; }
        int WeaponStrength { get; }
        int WeaponArmourPen { get; }
        int WeaponDamage { get; }
    }
}
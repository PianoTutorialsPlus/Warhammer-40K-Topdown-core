namespace WH40K.PlayerEvents
{
    public interface IUnitStats
    {
        public Fraction Fraction { get; }
        public int Movement { get; }
        int BallisticSkill { get; }
        int Toughness { get; }
        int ArmourSave { get; }
        int Wounds { get; }
    }
}
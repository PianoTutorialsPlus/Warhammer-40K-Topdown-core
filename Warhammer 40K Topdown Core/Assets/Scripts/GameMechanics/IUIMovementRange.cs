namespace WH40K.UI
{
    public interface IUIMovementRange
    {
        IndicatorUIEventChannelSO IndicatorConnectionUIEvent { get; }
        BattleroundEventChannelSO SetPhaseEvent { get; }
    }
}
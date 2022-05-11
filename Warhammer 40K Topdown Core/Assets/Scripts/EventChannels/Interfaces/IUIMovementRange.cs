namespace WH40K.EventChannels
{
    public interface IUIMovementRange
    {
        IndicatorUIEventChannelSO IndicatorConnectionUIEvent { get; }
        BattleroundEventChannelSO SetPhaseEvent { get; }
    }
}
namespace WH40K.UI
{
    public interface IUIMovementRange
    {
        IndicatorUIEventChannelSO IndicatorConnectionUIEvent { get; set; }
        BattleroundEventChannelSO SetPhaseEvent { get; set; }
    }
}
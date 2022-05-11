namespace WH40K.EventChannels
{
    public interface IManageUIEvents : IUIMovementRange
    {
        InteractionUIEventChannelSO InteractionUIEvent { get; }
        InfoUIEventChannelSO InfoUIEvent { get; }
        InfoUIEventChannelSO EnemyInfoUIEvent { get; }
    }
}
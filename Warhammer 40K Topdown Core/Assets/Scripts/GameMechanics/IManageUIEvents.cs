namespace WH40K.UI
{
    public interface IManageUIEvents: IUIMovementRange
    {
        InteractionUIEventChannelSO InteractionUIEvent { get; }
        InfoUIEventChannelSO InfoUIEvent { get; }
        InfoUIEventChannelSO EnemyInfoUIEvent { get;  }
    }
}
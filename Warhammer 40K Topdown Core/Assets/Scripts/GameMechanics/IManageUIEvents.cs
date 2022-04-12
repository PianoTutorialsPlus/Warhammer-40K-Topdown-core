namespace WH40K.UI
{
    public interface IManageUIEvents
    {
        InteractionUIEventChannelSO InteractionUIEvent { get; }
        InfoUIEventChannelSO InfoUIEvent { get; }
        InfoUIEventChannelSO EnemyInfoUIEvent { get;  }
    }
}
using WH40K.Gameplay.EventChannels;
using WH40K.Stats.Player;
using WH40K.Stats;

namespace WH40K.Gameplay.Events
{
    public class UIMovementRangeEvents
    {
        private GameStatsSO _gameStats;
        public BattleroundEventChannelSO _setPhaseEvent;
        public IndicatorUIEventChannelSO _toggleIndicatorConnectionUI;
        public Fraction _playerFraction => _gameStats.ActivePlayer.Fraction;

        public UIMovementRangeEvents(
            GameStatsSO gameStats,
            BattleroundEventChannelSO phaseEvent,
            IndicatorUIEventChannelSO indicatorConnectionUIEvent)
        {
            _gameStats = gameStats;
            _setPhaseEvent = phaseEvent;
            _toggleIndicatorConnectionUI = indicatorConnectionUIEvent;
        }
        public void SetIndicatorConnection(IUnit child)
        {
            if (ConnectRangeIndicator(child)) child.OnTapDownAction += ConnectIndicator;
            else ResetOnTapDownAction(child);
        }
        private bool ConnectRangeIndicator(IUnit child)
        {
            return _playerFraction == child.Fraction &&
                !child.IsDone && !child.IsActivated;
        }
        public void ConnectIndicator(IUnit unit)
        {
            _toggleIndicatorConnectionUI.RaiseEvent(true, unit);
        }
        public void ResetOnTapDownAction(IUnit child)
        {
            child.OnTapDownAction -= ConnectIndicator;
        }
    }
}
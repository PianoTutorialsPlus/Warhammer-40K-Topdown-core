using WH40K.Essentials;

namespace WH40K.UI
{
    public class UIMovementRangeEvents
    {
        private GameStatsSO _gameStats;
        private IUIMovementRange _uIMovementRange;

        public BattleroundEventChannelSO _setPhaseEvent => _uIMovementRange.SetPhaseEvent;
        public IndicatorUIEventChannelSO _toggleIndicatorConnectionUI => _uIMovementRange.IndicatorConnectionUIEvent;
        public Fraction ActivePlayerFraction => _gameStats.ActivePlayer.Fraction;
        public bool IsUnitDone(Unit child) => child.IsDone;
        public bool IsUnitActivated(Unit child) => child.IsActivated;

        public UIMovementRangeEvents(IUIMovementRange uIMovementRange, GameStatsSO gameStats)
        {
            _uIMovementRange = uIMovementRange;
            _gameStats = gameStats;
        }
        public void SetIndicatorConnection(Unit child)
        {
            if (ConnectRangeIndicator(child)) child.OnTapDownAction += ConnectIndicator;
            else ResetOnTapDownAction(child);
        }
        private bool ConnectRangeIndicator(Unit child)
        {
            return ActivePlayerFraction == child.Fraction &&
                !IsUnitDone(child) && !IsUnitActivated(child);
        }
        private void ConnectIndicator(Unit unit)
        {
            _setPhaseEvent.RaiseEvent(_gameStats);
            _toggleIndicatorConnectionUI.RaiseEvent(true, unit);
        }
        public void ResetOnTapDownAction(Unit child)
        {
            child.OnTapDownAction -= ConnectIndicator;
        }
    }
}
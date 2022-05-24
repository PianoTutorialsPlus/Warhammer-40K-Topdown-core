using WH40K.Core;
using WH40K.EventChannels;
using WH40K.PlayerEvents;

namespace WH40K.Events
{
    public class UIMovementRangeEvents
    {
        private IUIMovementRange _uIMovementRange;

        public BattleroundEventChannelSO _setPhaseEvent;/* => _uIMovementRange.SetPhaseEvent;*/
        public IndicatorUIEventChannelSO _toggleIndicatorConnectionUI;/* => _uIMovementRange.IndicatorConnectionUIEvent;*/
        public Fraction _playerFraction => GameStats.ActivePlayer.Fraction;

        public UIMovementRangeEvents(
            BattleroundEventChannelSO phaseEvent,
            IndicatorUIEventChannelSO indicatorConnectionUIEvent)
        {
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
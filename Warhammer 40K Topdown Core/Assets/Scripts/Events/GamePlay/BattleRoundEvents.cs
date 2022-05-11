using WH40K.Core;
using WH40K.EventChannels;
using WH40K.PlayerEvents;

namespace WH40K.Events
{
    public class BattleRoundEvents
    {
        private IUIMovementRange _uIMovementRange;

        public BattleroundEventChannelSO _setPhaseEvent => _uIMovementRange.SetPhaseEvent;
        public Fraction _playerFraction => GameStats.ActivePlayer.Fraction;

        public BattleRoundEvents(IUIMovementRange uIMovementRange)
        {
            _uIMovementRange = uIMovementRange;
        }
        public void SetPhaseEvent(IUnit child)
        {
            if (ConnectPhaseEvent(child)) child.OnTapDownAction += ConnectEvent;
            else ResetOnTapDownAction(child);
        }
        private bool ConnectPhaseEvent(IUnit child)
        {
            return _playerFraction == child.Fraction &&
                !child.IsDone && !child.IsActivated;
        }
        public void ConnectEvent(IUnit unit)
        {
            _setPhaseEvent.RaiseEvent();
        }
        public void ResetOnTapDownAction(IUnit child)
        {
            child.OnTapDownAction -= ConnectEvent;
        }
    }

}
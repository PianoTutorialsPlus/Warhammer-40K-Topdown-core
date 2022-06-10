using WH40K.Gameplay.EventChannels;
using WH40K.Stats.Player;
using WH40K.Stats;

namespace WH40K.Gameplay.Events
{
    public class BattleRoundEvents
    {
        private GameStatsSO _gameStats;
        public BattleroundEventChannelSO _setPhaseEvent;
        public Fraction _playerFraction => _gameStats.ActivePlayer.Fraction;

        public BattleRoundEvents(
            BattleroundEventChannelSO phaseEvent,
            GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            _setPhaseEvent = phaseEvent;
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
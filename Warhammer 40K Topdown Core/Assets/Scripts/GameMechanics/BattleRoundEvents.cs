using WH40K.Essentials;
using WH40K.UI;

namespace WH40K.GameMechanics
{
    public class BattleRoundEvents
    {
            private GameStatsSO _gameStats;
            private IUIMovementRange _uIMovementRange;

            public BattleroundEventChannelSO _setPhaseEvent => _uIMovementRange.SetPhaseEvent;
            public Fraction _playerFraction => _gameStats.ActivePlayer.Fraction;

            public BattleRoundEvents(IUIMovementRange uIMovementRange, GameStatsSO gameStats)
            {
                _uIMovementRange = uIMovementRange;
                _gameStats = gameStats;
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
                _setPhaseEvent.RaiseEvent(_gameStats);
            }
            public void ResetOnTapDownAction(IUnit child)
            {
                child.OnTapDownAction -= ConnectEvent;
            }
        }
    
}
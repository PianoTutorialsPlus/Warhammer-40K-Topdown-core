using WH40K.Gameplay.EventChannels;
using WH40K.Stats.Player;
using WH40K.Stats;

namespace WH40K.Gameplay.Events
{
    public class UIDisplayInfoEvents
    {
        private Fraction _playerFraction => _gameStats.ActivePlayer.Fraction;

        private GameStatsSO _gameStats;
        private InfoUIEventChannelSO _toggleInfoUI;
        private InfoUIEventChannelSO _toggleEnemyInfoUI;

        public UIDisplayInfoEvents(
            GameStatsSO gameStats,
            InfoUIEventChannelSO infoUIEvent,
            InfoUIEventChannelSO enemyInfoUIEvent)
        {
            _gameStats = gameStats;
            _toggleInfoUI = infoUIEvent;
            _toggleEnemyInfoUI = enemyInfoUIEvent;
        }

        public void SetDisplayInfo(IUnit child)
        {
            if (DisplayInfo(child)) child.OnPointerEnterInfo += DisplayInfoUI;
            else ResetOnPointerEnterInfo(child);
        }
        private bool DisplayInfo(IUnit child)
        {
            return !(_playerFraction == child.Fraction && child.IsDone);
        }
        public void DisplayInfoUI(IStats unit)
        {
            if (unit.Fraction == _playerFraction)
                _toggleInfoUI.RaiseEvent(true, unit);
            else
                _toggleEnemyInfoUI.RaiseEvent(true, unit);
        }

        public void SetResetInteraction(IUnit child)
        {
            child.OnPointerExit += ResetInteraction;
        }
        private void ResetInteraction(IUnit unit)
        {
            if (!unit.IsActivated) _toggleInfoUI.RaiseEvent(false, unit);
            _toggleEnemyInfoUI.RaiseEvent(false, unit);
        }
        public void ResetOnPointerEnterInfo(IUnit child)
        {
            child.OnPointerEnterInfo -= DisplayInfoUI;
        }
        public void ResetOnPointerExit(IUnit child)
        {
            child.OnPointerExit -= ResetInteraction;
        }
    }
}
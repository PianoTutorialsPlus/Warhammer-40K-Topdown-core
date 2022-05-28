﻿using WH40K.Core;
using WH40K.EventChannels;
using WH40K.PlayerEvents;

namespace WH40K.Events
{
    public class UIDisplayInfoEvents
    {
        private Fraction _playerFraction => GameStats.ActivePlayer.Fraction;
        private InfoUIEventChannelSO _toggleInfoUI;
        private InfoUIEventChannelSO _toggleEnemyInfoUI;

        public UIDisplayInfoEvents(
            InfoUIEventChannelSO infoUIEvent,
            InfoUIEventChannelSO enemyInfoUIEvent)
        {
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
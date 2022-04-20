using System;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.UI
{
    public class UIDisplayInteractionEvents
    {
        private IManageUIEvents _uIEvents;
        private GameStatsSO _gameStats;
        private Fraction _playerFraction => _gameStats.ActivePlayer.Fraction;
        private IStats _activeUnit => _gameStats.activeUnitTest;
        public bool IsUnitActive(IStats child) => child == _activeUnit;
        private InteractionUIEventChannelSO _toggleInteractionUI => _uIEvents.InteractionUIEvent;
        
        public UIDisplayInteractionEvents(IManageUIEvents uIEvents,GameStatsSO gameStats)
        {
            _uIEvents = uIEvents;
            _gameStats = gameStats;
        }

        public void SetDisplayInteraction(IUnit child)
        {
            if (DisplayInteraction(child)) child.OnPointerEnter += DisplayInteractionUI;
            else ResetOnPointerEnter(child);
        }
        private bool DisplayInteraction(IStats child)
        {
            return _playerFraction == child.Fraction &&
                !child.IsDone && !child.IsActivated && IsUnitActive(child);
        }
        public void DisplayInteractionUI()
        {
            //Raise event to display UI
            _toggleInteractionUI.RaiseEvent(true, InteractionType.Activate);
        }
        public void ResetOnPointerEnter(IUnit child)
        {
            child.OnPointerEnter -= DisplayInteractionUI;
        }
        public void SetResetInteraction(IUnit child)
        {
            child.OnPointerExit += ResetInteraction;
        }

        private void ResetInteraction(IUnit unit)
        {
            if (!IsUnitActive(unit)) _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
        }

        public void ResetOnPointerExit(IUnit child)
        {
            child.OnPointerExit -= ResetInteraction;
        }
    }
}
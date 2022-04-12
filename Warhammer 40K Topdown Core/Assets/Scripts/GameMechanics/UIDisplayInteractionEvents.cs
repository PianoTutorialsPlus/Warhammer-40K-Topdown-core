using System;
using WH40K.Essentials;

namespace WH40K.UI
{
    public class UIDisplayInteractionEvents
    {
        private IManageUIEvents _uIEvents;
        private GameStatsSO _gameStats;
        public bool IsUnitDone(Unit child) => child.IsDone;
        private Fraction ActivePlayerFraction => _gameStats.ActivePlayer.Fraction;
        public bool IsUnitActive(Unit child) => child as IStats == _gameStats.activeUnitTest;
        private InteractionUIEventChannelSO _toggleInteractionUI => _uIEvents.InteractionUIEvent;
        
        public UIDisplayInteractionEvents(IManageUIEvents uIEvents, GameStatsSO gameStats)
        {
            _uIEvents = uIEvents;
            _gameStats = gameStats;
        }

        public void SetDisplayInteraction(Unit child)
        {
            if (DisplayInteraction(child)) child.OnPointerEnter += DisplayInteractionUI;
            else ResetOnPointerEnter(child);
        }
        private bool DisplayInteraction(Unit child)
        {
            return ActivePlayerFraction == child.Fraction &&
                !IsUnitDone(child) && IsUnitActive(child);
        }
        private void DisplayInteractionUI()
        {
            //Raise event to display UI
            _toggleInteractionUI.RaiseEvent(true, InteractionType.Activate);
        }
        public void ResetOnPointerEnter(Unit child)
        {
            child.OnPointerEnter -= DisplayInteractionUI;
        }
        public void SetResetInteraction(IUnit child)
        {
            child.OnPointerExit += ResetInteraction;
        }

        private void ResetInteraction(IUnit unit)
        {
            if (!unit.IsSelected) _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
        }

        public void ResetOnPointerExit(Unit child)
        {
            child.OnPointerExit -= ResetInteraction;
        }
    }
}
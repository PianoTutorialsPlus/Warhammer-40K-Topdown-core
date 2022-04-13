using System;
using WH40K.Essentials;

namespace WH40K.UI
{
    public class UIDisplayInteractionEvents
    {
        private IManageUIEvents _uIEvents;
        private Fraction _playerFraction;
        private IStats _activeUnit;
        public bool IsUnitDone(IUnitCondition child) => child.IsDone;
        public bool IsUnitActive(IStats child) => child == _activeUnit;
        private InteractionUIEventChannelSO _toggleInteractionUI => _uIEvents.InteractionUIEvent;
        
        public UIDisplayInteractionEvents(IManageUIEvents uIEvents, Fraction playerFraction, IStats activeUnit)
        {
            _uIEvents = uIEvents;
            _playerFraction = playerFraction;
            _activeUnit = activeUnit;
        }

        public void SetDisplayInteraction(IUnit child)
        {
            if (DisplayInteraction(child)) child.OnPointerEnter += DisplayInteractionUI;
            else ResetOnPointerEnter(child);
        }
        private bool DisplayInteraction(IStats child)
        {
            return _playerFraction == child.Fraction &&
                !IsUnitDone(child) && IsUnitActive(child) && !child.IsActivated;
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
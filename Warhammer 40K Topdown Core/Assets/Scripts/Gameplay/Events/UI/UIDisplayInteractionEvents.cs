using WH40K.Gameplay.EventChannels;
using WH40K.Gameplay.Core;
using WH40K.Stats.Player;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Gameplay.PlayerEvents;

namespace WH40K.Gameplay.Events
{
    public class UIDisplayInteractionEvents
    {
        private Fraction _playerFraction => GameStats.ActivePlayer.Fraction;
        private IStats _activeUnit => GameStats.ActiveUnit;
        public bool IsUnitActive(IStats child) => child == _activeUnit;
        private InteractionUIEventChannelSO _toggleInteractionUI;

        public UIDisplayInteractionEvents(InteractionUIEventChannelSO interactionUIEvent)
        {
            _toggleInteractionUI = interactionUIEvent;
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
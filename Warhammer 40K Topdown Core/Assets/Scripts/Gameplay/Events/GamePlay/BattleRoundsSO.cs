using WH40K.Gameplay.Core;
using WH40K.Gameplay.PlayerEvents;

namespace WH40K.Gameplay.Events
{
    public class BattleRoundsSO : IPhase
    {
        private UIDisplayInteractionEvents _uIDisplayInteractionEvents;
        private UIDisplayInfoEvents _uIDisplayInfoEvents;
        private UIMovementRangeEvents _uIMovementRange;
        private BattleRoundEvents _battleRoundEvents;

        public BattleRoundsSO(
            UIDisplayInteractionEvents uIDisplayInteractionEvents,
            UIDisplayInfoEvents uIDisplayInfoEvents,
            UIMovementRangeEvents uIMovementRange,
            BattleRoundEvents battleRoundEvents)
        {
            _uIDisplayInteractionEvents = uIDisplayInteractionEvents;
            _uIDisplayInfoEvents = uIDisplayInfoEvents;
            _uIMovementRange = uIMovementRange;
            _battleRoundEvents = battleRoundEvents;
        }

        private IPlayer _activePlayer => GameStats.ActivePlayer;
        private IPlayer _enemyPlayer => GameStats.EnemyPlayer;

        public void HandlePhase()
        {
            foreach (UnitFacade child in _activePlayer.PlayerUnits) FillMethods(child);
            foreach (UnitFacade child in _enemyPlayer.PlayerUnits) FillMethods(child);
        }
        public void ClearPhase()
        {
            foreach (UnitFacade child in _activePlayer.PlayerUnits) ResetMethods(child);
            foreach (UnitFacade child in _enemyPlayer.PlayerUnits) ResetMethods(child);
        }
        public void FillMethods(UnitFacade child)
        {
            _uIDisplayInteractionEvents.SetResetInteraction(child);
            _uIDisplayInfoEvents.SetResetInteraction(child);

            _uIDisplayInteractionEvents.SetDisplayInteraction(child);
            _uIDisplayInfoEvents.SetDisplayInfo(child);
            _uIMovementRange.SetIndicatorConnection(child);
            _battleRoundEvents.SetPhaseEvent(child);
        }
        public void ResetMethods(UnitFacade child)
        {
            _uIDisplayInfoEvents.ResetOnPointerExit(child);
            _uIDisplayInteractionEvents.ResetOnPointerExit(child);

            _uIMovementRange.ResetOnTapDownAction(child);
            _uIDisplayInteractionEvents.ResetOnPointerEnter(child);
            _uIDisplayInfoEvents.ResetOnPointerEnterInfo(child);
            _battleRoundEvents.ResetOnTapDownAction(child);
        }
    }
}
using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats;
using WH40K.Stats.Player;

namespace WH40K.Gameplay.Events
{
    public class BattleRoundsSO : IPhase
    {
        private GameStatsSO _gameStats;
        private UIDisplayInteractionEvents _uIDisplayInteractionEvents;
        private UIDisplayInfoEvents _uIDisplayInfoEvents;
        private UIMovementRangeEvents _uIMovementRange;
        private BattleRoundEvents _battleRoundEvents;

        public BattleRoundsSO(
            GameStatsSO gameStats,
            UIDisplayInteractionEvents uIDisplayInteractionEvents,
            UIDisplayInfoEvents uIDisplayInfoEvents,
            UIMovementRangeEvents uIMovementRange,
            BattleRoundEvents battleRoundEvents)
        {
            _gameStats = gameStats;
            _uIDisplayInteractionEvents = uIDisplayInteractionEvents;
            _uIDisplayInfoEvents = uIDisplayInfoEvents;
            _uIMovementRange = uIMovementRange;
            _battleRoundEvents = battleRoundEvents;
        }

        private PlayerSO _activePlayer => _gameStats.ActivePlayer;
        private PlayerSO _enemyPlayer => _gameStats.EnemyPlayer;

        public void HandlePhase()
        {
            foreach (GameObject child in _activePlayer.PlayerUnits) FillMethods(child);
            foreach (GameObject child in _enemyPlayer.PlayerUnits) FillMethods(child);
        }
        public void ClearPhase()
        {
            foreach (GameObject child in _activePlayer.PlayerUnits) ResetMethods(child);
            foreach (GameObject child in _enemyPlayer.PlayerUnits) ResetMethods(child);
        }
        public void FillMethods(GameObject child)
        {
            var unitFacade = child.GetComponent<UnitFacade>();

            _uIDisplayInteractionEvents.SetResetInteraction(unitFacade);
            _uIDisplayInfoEvents.SetResetInteraction(unitFacade);

            _uIDisplayInteractionEvents.SetDisplayInteraction(unitFacade);
            _uIDisplayInfoEvents.SetDisplayInfo(unitFacade);
            _uIMovementRange.SetIndicatorConnection(unitFacade);
            _battleRoundEvents.SetPhaseEvent(unitFacade);
        }
        public void ResetMethods(GameObject child)
        {
            var unitFacade = child.GetComponent<UnitFacade>();

            _uIDisplayInfoEvents.ResetOnPointerExit(unitFacade);
            _uIDisplayInteractionEvents.ResetOnPointerExit(unitFacade);

            _uIMovementRange.ResetOnTapDownAction(unitFacade);
            _uIDisplayInteractionEvents.ResetOnPointerEnter(unitFacade);
            _uIDisplayInfoEvents.ResetOnPointerEnterInfo(unitFacade);
            _battleRoundEvents.ResetOnTapDownAction(unitFacade);
        }
    }
}
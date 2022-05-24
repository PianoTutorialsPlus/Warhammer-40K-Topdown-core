using UnityEngine;
using WH40K.Core;
using WH40K.EventChannels;
using WH40K.PlayerEvents;

namespace WH40K.Events
{
    [CreateAssetMenu(menuName = "Game/Battleround Events")]
    public class BattleRoundsSO : ScriptableObject, IPhase, IManageUIEvents, IUIMovementRange
    {
        [SerializeField] private BattleroundEventChannelSO _setPhaseEvent;

        //UI event
        [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;
        [SerializeField] private InfoUIEventChannelSO _toggleInfoUI = default;
        [SerializeField] private InfoUIEventChannelSO _toggleEnemyInfoUI = default;
        [SerializeField] private IndicatorUIEventChannelSO _toggleIndicatorConnectionUI = default;

        private UIDisplayInteractionEvents _uIDisplayInteractionEvents;
        private UIDisplayInfoEvents _uIDisplayInfoEvents;
        private UIMovementRangeEvents _uIMovementRange;
        private BattleRoundEvents _battleRoundEvents;
        private IPlayer _activePlayer => GameStats.ActivePlayer;
        private IPlayer _enemyPlayer => GameStats.EnemyPlayer;

        public InteractionUIEventChannelSO InteractionUIEvent => _toggleInteractionUI;
        public InfoUIEventChannelSO InfoUIEvent => _toggleInfoUI;
        public InfoUIEventChannelSO EnemyInfoUIEvent => _toggleEnemyInfoUI;
        public IndicatorUIEventChannelSO IndicatorConnectionUIEvent => _toggleIndicatorConnectionUI;
        public BattleroundEventChannelSO SetPhaseEvent => _setPhaseEvent;

        public void OnEnable()
        {
            _uIDisplayInteractionEvents = new UIDisplayInteractionEvents(this);
            _uIDisplayInfoEvents = new UIDisplayInfoEvents(this);
            _uIMovementRange = new UIMovementRangeEvents(this);
            _battleRoundEvents = new BattleRoundEvents(this);
        }
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
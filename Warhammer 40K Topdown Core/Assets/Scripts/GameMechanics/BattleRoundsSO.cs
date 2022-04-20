using UnityEngine;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace WH40K.UI
{
    [CreateAssetMenu(menuName = "Game/Battleround Events")]
    public class BattleRoundsSO : ScriptableObject, IPhase, IManageUIEvents, IUIMovementRange
    {
        [SerializeField] private GameStatsSO _gameStats;
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
        private IPlayer _activePlayer => _gameStats.ActivePlayer;
        private IPlayer _enemyPlayer => _gameStats.EnemyPlayer;

        public InteractionUIEventChannelSO InteractionUIEvent { get => _toggleInteractionUI; set => _toggleInteractionUI = value; }
        public InfoUIEventChannelSO InfoUIEvent { get => _toggleInfoUI; set => _toggleInfoUI = value; }
        public InfoUIEventChannelSO EnemyInfoUIEvent { get => _toggleEnemyInfoUI; set => _toggleEnemyInfoUI = value; }
        public IndicatorUIEventChannelSO IndicatorConnectionUIEvent { get => _toggleIndicatorConnectionUI; set => _toggleIndicatorConnectionUI = value; }
        public BattleroundEventChannelSO SetPhaseEvent { get => _setPhaseEvent; set => _setPhaseEvent = value; }

        public void OnEnable()
        {
            _uIDisplayInteractionEvents = new UIDisplayInteractionEvents(this, _gameStats);
            _uIDisplayInfoEvents = new UIDisplayInfoEvents(this, _gameStats);
            _uIMovementRange = new UIMovementRangeEvents(this, _gameStats);
            _battleRoundEvents = new BattleRoundEvents(this, _gameStats);
        }
        public void HandlePhase(GameStatsSO gameStats)
        {
            foreach (Unit child in _activePlayer.PlayerUnits) FillMethods(child);
            foreach (Unit child in _enemyPlayer.PlayerUnits) FillMethods(child);
        }
        public void ClearPhase(GameStatsSO gameStats)
        {
            foreach (Unit child in _activePlayer.PlayerUnits) ResetMethods(child);
            foreach (Unit child in _enemyPlayer.PlayerUnits) ResetMethods(child);

        }
        public void FillMethods(Unit child)
        {
            _uIDisplayInteractionEvents.SetResetInteraction(child);
            _uIDisplayInfoEvents.SetResetInteraction(child);

            _uIDisplayInteractionEvents.SetDisplayInteraction(child);
            _uIDisplayInfoEvents.SetDisplayInfo(child);
            _uIMovementRange.SetIndicatorConnection(child);
            _battleRoundEvents.SetPhaseEvent(child);
        }
        public void ResetMethods(Unit child)
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
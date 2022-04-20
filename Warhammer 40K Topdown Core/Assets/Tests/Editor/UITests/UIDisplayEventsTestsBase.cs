using Editor.Infrastructure;
using UnityEngine.Events;
using WH40K.Essentials;
using WH40K.UI;

namespace Editor.UI
{
    public abstract class UIDisplayEventsTestsBase
    {
        public UnityAction _pointerAction;
        public UnityAction<IUnit> _action;
        public bool _state;
        public void FillWithStats(bool state, IStats stats)
        {
            _state = state;
        }
        public void FillWithStats(bool state, InteractionType stats)
        {
            _state = state;
        }
        public void FillWithStats(GameStatsSO gameStats)
        {
            _state = true;
        }

        public GameStatsSO GetGameStats(Fraction playerFraction = Fraction.Necrons, IUnit activeUnit = null)
        {
            return A.GameStats
                .WithActiveUnit(activeUnit)
                .WithActivePlayer(A.Player.WithFraction(playerFraction));
        }
        public InfoUIEventChannelSO GetInfoEventListener()
        {
            InfoUIEventChannelSO eventListener = A.InfoUIEventChannel;
            eventListener.OnEventRaised += FillWithStats;
            return eventListener;
        }
        public InteractionUIEventChannelSO GetInteractionEventListener()
        {
            InteractionUIEventChannelSO eventListener = A.InteractionUIEventChannel;
            eventListener.OnEventRaised += FillWithStats;
            return eventListener;
        }
        public IndicatorUIEventChannelSO GetIndicatorEventListener()
        {
            IndicatorUIEventChannelSO eventListener = A.IndicatorUIEventChannel;
            eventListener.OnEventRaised += FillWithStats;
            return eventListener;
        }
        public BattleroundEventChannelSO GetBattleRoundEventListener()
        {
            BattleroundEventChannelSO eventListener = A.BattleRoundEventChannel;
            eventListener.OnEventRaised += FillWithStats;
            return eventListener;
        }
        
        public IUnit GetUnit(Fraction playerFraction = Fraction.Necrons, bool isActivated = false, bool isDone = false)
        {
            return A.Unit
                    .WithOnPointerEnterInfo(_action)
                    .WithOnPointerEnter(_pointerAction)
                    .WithOnTapdownAction(_action)
                    .WithOnPointerExit(_action)
                    .WithFraction(playerFraction)
                    .WithIsActivatedState(isActivated)
                    .WithIsDoneState(isDone)
                    .Build();
        }
    }
}
using Editor.Infrastructure;
using UnityEngine;
using WH40K.Gameplay.EventChannels;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats.Player;

namespace Editor.Base
{
    public abstract class UIDisplayEventsTestsBase : CoreElementsBase
    {
        public bool _state;
        public void FillWithStats(bool state, IStats stats)
        {
            _state = state;
        }
        public void FillWithStats(bool state, InteractionType stats)
        {
            _state = state;
        }
        public void FillWithStats()
        {
            _state = true;
        }

        public T GetEventListener<T>() where T: ScriptableObject
        {
            T eventListener = A.EventChannel<T>();
            return eventListener;
        }

        public InfoUIEventChannelSO GetInfoEventListener()
        {
            var eventListener = GetEventListener<InfoUIEventChannelSO>();
            eventListener.OnEventRaised += FillWithStats;
            return eventListener;
        }
        public InteractionUIEventChannelSO GetInteractionEventListener()
        {
            var eventListener = GetEventListener<InteractionUIEventChannelSO>();
            eventListener.OnEventRaised += FillWithStats;
            return eventListener;
        }
        public IndicatorUIEventChannelSO GetIndicatorEventListener()
        {
            var eventListener = GetEventListener<IndicatorUIEventChannelSO>();
            eventListener.OnEventRaised += FillWithStats;
            return eventListener;
        }
        public BattleroundEventChannelSO GetBattleRoundEventListener()
        {
            var eventListener = GetEventListener<BattleroundEventChannelSO>();
            eventListener.OnEventRaised += FillWithStats;
            return eventListener;
        }
    }
}
using System;
using UnityEngine;
using WH40K.EventChannels;
using WH40K.Events;
using Zenject;

namespace WH40K.Installers
{
    public class EventInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;

        public override void InstallBindings()
        {
            Container.Bind<UIDisplayInfoEvents>().AsSingle()
                .WithArguments(_settings.InfoUIEvent,_settings.EnemyInfoUIEvent);
            Container.Bind<UIDisplayInteractionEvents>().AsSingle()
                .WithArguments(_settings.InteractionUIEvent);
            Container.Bind<UIMovementRangeEvents>().AsSingle()
                .WithArguments(_settings.PhaseEvent, _settings.IndicatorConnectionUIEvent);
            Container.Bind<BattleRoundEvents>().AsSingle()
                .WithArguments(_settings.PhaseEvent);

            Container.BindInterfacesAndSelfTo<BattleRoundsSO>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public InteractionUIEventChannelSO InteractionUIEvent;
            public InfoUIEventChannelSO InfoUIEvent;
            public InfoUIEventChannelSO EnemyInfoUIEvent;
            public BattleroundEventChannelSO PhaseEvent;
            public IndicatorUIEventChannelSO IndicatorConnectionUIEvent;
        }
    }
}

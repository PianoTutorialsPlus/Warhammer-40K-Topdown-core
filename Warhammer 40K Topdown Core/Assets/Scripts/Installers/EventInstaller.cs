using System;
using UnityEngine;
using WH40K.Gameplay.EventChannels;
using WH40K.Gameplay.Events;
using Zenject;

namespace WH40K.Installers
{
    public class EventInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;

        public override void InstallBindings()
        {
            Container.BindInstance(_settings.InteractionUIEvent).AsSingle();
            Container.BindInstance(_settings.PhaseEvent).AsSingle();
            Container.BindInstance(_settings.IndicatorConnectionUIEvent).AsSingle();

            Container.BindInstance(_settings.GameinfoUIEvent).AsSingle();
            Container.BindInstance(_settings.GameStatsEvent).AsSingle();

            Container.Bind<InfoUIEventChannelSO>().WithId("Player").FromInstance(_settings.InfoUIEvent);
            Container.Bind<InfoUIEventChannelSO>().WithId("Enemy").FromInstance(_settings.EnemyInfoUIEvent);


            Debug.Log("EventInstaller");

            Container.Bind<UIDisplayInfoEvents>().AsSingle();
            Container.Bind<UIDisplayInteractionEvents>().AsSingle();
            Container.Bind<UIMovementRangeEvents>().AsSingle();
            Container.Bind<BattleRoundEvents>().AsSingle();

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

            public GameInfoUIEventChannelSO GameinfoUIEvent;
            public GameStatsEventChannelSO GameStatsEvent;
        }
    }
}

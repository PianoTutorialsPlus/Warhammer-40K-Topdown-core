using System;
using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats;
using WH40K.Stats.Player;
using Zenject;

namespace WH40K.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;

        public TableSettings Table;

        [Serializable]
        public class TableSettings
        {
            public GameTableSO.Settings TableHandler;
        }
        public override void InstallBindings()
        {
            Container.BindInstance(_settings.GameTable).AsSingle();
            Container.BindInstance(Table.TableHandler).IfNotBound();
            Container.QueueForInject(_settings.GameTable);

            Container.BindInterfacesAndSelfTo<UnitSpawner>().AsSingle();

            Container.BindFactory<Fraction, UnitFacade, UnitFacade.Factory>().FromFactory<UnitFactory>();


        }
        [Serializable]
        public class Settings
        {
            public GameTableSO GameTable;
        }

        //class UnitFacadePool : MonoPoolableMemoryPool<IMemoryPool, UnitFacade>
        //{
        //}


    }
}
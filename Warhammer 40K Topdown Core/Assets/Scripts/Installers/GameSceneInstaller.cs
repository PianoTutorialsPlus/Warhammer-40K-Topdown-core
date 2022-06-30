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
            Clear();

            Container.Bind<PlayerSO>().FromInstance(_settings.Player1);
            Container.Bind<PlayerSO>().FromInstance(_settings.Player2);

            Container.BindInstance(_settings.GameTable).AsSingle();
            Container.BindInstance(Table.TableHandler).IfNotBound();
            Container.QueueForInject(_settings.GameTable);

        }

        private void Clear()
        {
            _settings.Player1.PlayerUnits.Clear();
            _settings.Player2.PlayerUnits.Clear();
        }

        [Serializable]
        public class Settings
        {
            public GameTableSO GameTable;
            public PlayerSO Player1;
            public PlayerSO Player2;
        }

        //class UnitFacadePool : MonoPoolableMemoryPool<IMemoryPool, UnitFacade>
        //{
        //}


    }
}
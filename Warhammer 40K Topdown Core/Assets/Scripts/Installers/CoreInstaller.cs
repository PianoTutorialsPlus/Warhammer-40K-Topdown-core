using System;
using UnityEngine;
using WH40K.Stats;
using Zenject;

namespace WH40K.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;
        public override void InstallBindings()
        {
            Container.BindInstance(_settings.GameStats).AsSingle().NonLazy();
            Container.QueueForInject(_settings.GameStats);
        }


        [Serializable]
        public class Settings
        {
            public GameStatsSO GameStats;
        }
    }
}

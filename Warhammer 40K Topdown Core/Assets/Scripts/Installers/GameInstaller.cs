using System;
using UnityEngine;
using WH40K.EventChannels;
using WH40K.GamePhaseEvents;
using WH40K.PlayerEvents;
using WH40K.UI;
using Zenject;

namespace WH40K.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;

        public override void InstallBindings()
        {
            InstallGamePhases();
        }

        private void InstallGamePhases()
        {
            Debug.Log("and here?");
            Container.Bind<GamePhaseProcessor>().AsSingle().NonLazy();
            Container.Bind<MovementPhaseProcessor>().AsSingle().NonLazy();
            Container.Bind<ShootingPhaseProcessor>().AsSingle().NonLazy();
            Container.Bind<ShootingSubPhaseProcessor>().AsSingle().NonLazy();
            

            Container.Bind<UIRangeController>().AsSingle();

        }

        [Serializable]
        public class Settings
        {
            public PlayerSO _player1;
            public PlayerSO _player2;
        }
    }
}
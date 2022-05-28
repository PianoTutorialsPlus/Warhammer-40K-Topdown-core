using System;
using System.Collections.Generic;
using UnityEngine;
using WH40K;
using WH40K.EventChannels;
using WH40K.GamePhaseEvents;
using WH40K.InputEvents;
using WH40K.PlayerEvents;
using Zenject;

public class GamePhaseInstaller : MonoInstaller
{
    [SerializeField]
    Settings _settings = null;
    public override void InstallBindings()
    {
        Container.BindInstance(new List<PlayerSO> { _settings._player1, _settings._player2 });
        Container.BindInstance(_settings.InputReader).AsSingle();

        Container.BindInterfacesTo<Initializer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShootingPhaseManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShootingSubPhaseManager>().FromComponentInHierarchy().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public InputReader InputReader;

        public PlayerSO _player1;
        public PlayerSO _player2;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Gameplay;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Gameplay.PlayerEvents;
using WH40K.InputEvents;
using WH40K.Stats;
using WH40K.Stats.Player;
using Zenject;

public class GamePhaseInstaller : MonoInstaller
{
    [SerializeField]
    Settings _settings = null;
    public override void InstallBindings()
    {
        Container.BindInstance(new List<PlayerSO> { _settings.Player1, _settings.Player2 });
        Container.BindInstance(_settings.InputReader).AsSingle();

        Container.BindInterfacesTo<Initializer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShootingPhaseManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShootingSubPhaseManager>().FromComponentInHierarchy().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public InputReader InputReader;

        public PlayerSO Player1;
        public PlayerSO Player2;
    }
}
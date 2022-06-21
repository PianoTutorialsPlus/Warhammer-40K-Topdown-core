using System;
using UnityEngine;
using WH40K.Stats.Player;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    Settings _settings = null;

    public PlayerSettings Player;

    [Serializable]
    public class PlayerSettings
    {
        public PlayerSO.Settings PlayerHandler;
    }
    public override void InstallBindings()
    {
        Container.BindInstance(_settings.Player).AsSingle();
        Container.BindInstance(Player.PlayerHandler).IfNotBound();
        Container.QueueForInject(_settings.Player);
    }

    [Serializable]
    public class Settings
    {
        public PlayerSO Player;
    }
}
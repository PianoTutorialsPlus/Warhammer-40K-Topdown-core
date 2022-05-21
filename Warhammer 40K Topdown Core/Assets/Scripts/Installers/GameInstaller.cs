using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WH40K;
using WH40K.EventChannels;
using WH40K.GamePhaseEvents;
using Zenject;

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
        Container.Bind<CombatProcessor>().AsSingle().NonLazy();

    }

    [Serializable]
    public class Settings
    {
        public RollTheDiceEventChannelSO _diceAction;
        public RollTheDiceEventChannelSO _diceSubResult;
        public RollTheDiceEventChannelSO _diceResult;
    }
}

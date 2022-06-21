using System;
using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;
using Zenject;

namespace WH40K.Installers
{
    public class DiceActionInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;
        public override void InstallBindings()
        {
            Debug.Log("DiceInstaller");
            Container.Bind<IResult>().To<Settings>().AsSingle().NonLazy();
            Container.QueueForInject(_settings);

            //Container.Bind<RollTheDiceEventChannelSO>().WithId("Action").FromInstance(_settings.DiceAction);
            Container.Bind<RollTheDiceEventChannelSO>().WithId("Result").FromInstance(_settings.DiceResult);
            //Container.Bind<RollTheDiceEventChannelSO>().WithId("Sub Result").FromInstance(_settings.DiceSubResult);
            //Container.Bind<RollTheDiceEventChannelSO>().WithId("Test").FromInstance(_settings.DiceResult).AsSingle().Lazy();

            //Container.BindInstance(_settings.DiceSubResult).WithId("Sub Result").AsSingle();
            //Container.BindInstance(_settings.DiceResult).WithId("Result").AsSingle();

            Container.Bind<CombatProcessor>().AsSingle().WithArguments(_settings).NonLazy();
            //Container.BindInstance(_settings.DiceResult);
        }

        [Serializable]
        public class Settings : IResult
        {
            public RollTheDiceEventChannelSO _diceAction;
            public RollTheDiceEventChannelSO _diceSubResult;
            public RollTheDiceEventChannelSO _diceResult;
            public RollTheDiceEventChannelSO DiceAction => _diceAction;
            public RollTheDiceEventChannelSO DiceSubResult => _diceSubResult;
            public RollTheDiceEventChannelSO DiceResult => _diceResult;

        }
    }
}
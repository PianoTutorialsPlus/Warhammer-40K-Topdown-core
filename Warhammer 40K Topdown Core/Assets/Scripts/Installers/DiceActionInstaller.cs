using System;
using UnityEngine;
using WH40K.EventChannels;
using WH40K.GamePhaseEvents;
using Zenject;

public class DiceActionInstaller : MonoInstaller
{
    [SerializeField]
    Settings _settings = null;
    public override void InstallBindings()
    {
        Debug.Log("DiceInstaller");
        Container.Bind<IResult>().To<Settings>().AsSingle().NonLazy();

        Container.Bind<CombatProcessor>().AsSingle().WithArguments(_settings).NonLazy();
        Container.BindInstance(_settings.DiceResult);
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
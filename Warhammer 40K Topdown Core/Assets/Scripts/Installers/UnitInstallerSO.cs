using System;
using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using Zenject;

[CreateAssetMenu(fileName = "UnitInstallerSO", menuName = "Installers/UnitInstallerSO")]
public class UnitInstallerSO : ScriptableObjectInstaller<UnitInstallerSO>
{
    public FactorySettings Factory;

    [Serializable]
    public class FactorySettings
    {
        public UnitSpawner.Settings UnitSpawnerHandler;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(Factory.UnitSpawnerHandler).IfNotBound();
    }
}
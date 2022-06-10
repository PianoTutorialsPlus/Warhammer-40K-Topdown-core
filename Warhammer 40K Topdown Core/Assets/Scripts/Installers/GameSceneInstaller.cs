using System;
using UnityEngine;
using WH40K.Stats;
using Zenject;

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
        Container.BindInstance(_settings.GameTable).AsSingle();
        Container.BindInstance(Table.TableHandler).IfNotBound();
        Container.QueueForInject(_settings.GameTable);
    }
    [Serializable]
    public class Settings
    {
        public GameTableSO GameTable;
    }
}
using System;
using UnityEngine;
using WH40K.Gameplay.Core;
using WH40K.Stats;

namespace Editor.Infrastructure.GameStatss
{
    public class GameTableBuilder : TestDataBuilder<GameTableSO>
    {
        public override GameTableSO Build()
        {
            BindSettings();
            Container.Bind<GameTableSO>().AsSingle();
            //var gameTable =  ScriptableObject.CreateInstance<GameTableSO>();
            var gameTable = Container.Resolve<GameTableSO>();
            //var gameObject = new GameObject();
            //gameObject.AddComponent<GameTable>();
            //gameTable.GameTable = gameObject; 
            return gameTable;
        }

        private void BindSettings()
        {
            Container.Bind<GameTableSO.Settings>().AsSingle();
            var gameTableSettings = Container.Resolve<GameTableSO.Settings>();
            gameTableSettings.GameTable = Container.InstantiateComponentOnNewGameObject<GameTable>().gameObject;
        }
    }
}

using UnityEngine;
using WH40K.Gameplay.Core;
using WH40K.Stats;

namespace Editor.Infrastructure.GameStatss
{
    public class GameTableBuilder : TestDataBuilder<GameTableSO>
    {
        public override GameTableSO Build()
        {
            var gameTable = ScriptableObject.CreateInstance<GameTableSO>();
            var gameObject = new GameObject();
            gameObject.AddComponent<GameTable>();
            gameTable.GameTable = gameObject;
            return gameTable;
        }
    }
}

﻿using UnityEngine;
using WH40K.Essentials;

namespace Editor.Infrastructure.GameStatss
{
    public class GameTableBuilder : TestDataBuilder<GameTableSO>
    {
        public override GameTableSO Build()
        {
            var gameTable = ScriptableObject.CreateInstance<GameTableSO>();
            gameTable.gameTable = new GameObject().AddComponent<GameTable>();
            return gameTable;
        }
    }
}

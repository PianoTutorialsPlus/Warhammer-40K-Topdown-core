using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WH40K.Essentials;

namespace Editor.Infrastructure.GameStats
{
    public class GameTableBuilder : TestDataBuilder<GameTableSO>
    {
        public override GameTableSO Build()
        {
            var gameTable = ScriptableObject.CreateInstance<GameTableSO>();
            gameTable.gameTable = new GameTable();
            return gameTable;
        }
    }
}

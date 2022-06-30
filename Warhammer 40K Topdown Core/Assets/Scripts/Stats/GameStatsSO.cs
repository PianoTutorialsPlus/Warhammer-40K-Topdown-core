using System;
using UnityEngine;
using WH40K.Stats.Player;
using Zenject;

namespace WH40K.Stats
{
    public class GameStatsSO : ScriptableObject
    {
        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
            Enable();

        }
        public void Enable()
        {
            Turn = _settings.Turn;
            ActivePlayer = _settings.ActivePlayer;
            EnemyPlayer = _settings.EnemyPlayer;
            ActiveUnit = _settings.ActiveUnit;
            EnemyUnit = _settings.EnemyUnit;
            GameTable = _settings.GameTable;
            Phase = _settings.Phase;
        }

        private Settings _settings;
        public int Turn;/* { get; set; }*/
        public PlayerSO ActivePlayer; /*{ get; set; }*/
        public PlayerSO EnemyPlayer; /*{ get; set; }*/
        public IUnit ActiveUnit; /*{ get; set; }*/
        public IUnit EnemyUnit; /*{ get; set; }*/
        public GameTableSO GameTable;/* { get; set; }*/
        public GamePhase Phase;/* { get; set; }
*/
        [Serializable]
        public class Settings
        {
            public int Turn;
            public PlayerSO ActivePlayer;
            public PlayerSO EnemyPlayer;
            public IUnit ActiveUnit;
            public IUnit EnemyUnit;

            public GameTableSO GameTable;
            public GamePhase Phase;
        }
    }
}

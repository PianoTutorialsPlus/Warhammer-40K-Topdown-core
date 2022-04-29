using UnityEngine;

namespace WH40K.Essentials
{
    [CreateAssetMenu(menuName = "Game/Essential stats")]
    public class GameStatsSO : ScriptableObject, IGameStats
    {
        public GamePhase phase;
        public MovementPhase movementSubPhase;
        public ShootingPhase shootingSubPhase;
        public int turn;
        public Unit activeUnit;
        public Unit enemyUnit;
        [SerializeField] private PlayerSO _activePlayer;
        [SerializeField] private PlayerSO _enemyPlayer;
        [SerializeField] private GameTableSO gameTable;

        public PlayerSO ActivePlayer { get => _activePlayer; set => _activePlayer = value; }
        public PlayerSO EnemyPlayer { get => _enemyPlayer; set => _enemyPlayer = value; }

        public IUnit activeUnitTest;
        public IUnit enemyUnitTest;
        public IUnit ActiveUnit { get => activeUnitTest; set => activeUnitTest = value; }
        public IUnit EnemyUnit { get => enemyUnitTest; set => enemyUnitTest = value; }
        public GameTableSO GameTable { get => gameTable; set => gameTable = value; }
    }
}
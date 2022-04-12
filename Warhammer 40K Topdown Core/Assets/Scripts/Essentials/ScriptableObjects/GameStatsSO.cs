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

        public PlayerSO ActivePlayer { get => _activePlayer; set => _activePlayer = value; }
        public PlayerSO EnemyPlayer { get => _enemyPlayer; set => _enemyPlayer = value as PlayerSO; }
        public GameTableSO gameTable;

        public IStats activeUnitTest;
        public IStats enemyUnitTest;
    }
}
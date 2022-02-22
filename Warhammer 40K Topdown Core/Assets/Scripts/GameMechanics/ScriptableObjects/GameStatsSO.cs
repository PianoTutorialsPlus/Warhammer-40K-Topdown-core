using UnityEngine;

[CreateAssetMenu(menuName = "Game/Essential stats")]
public class GameStatsSO : ScriptableObject
{
    public GamePhase phase;
    public MovementPhase movementSubPhase;
    public ShootingPhase shootingSubPhase;
    public int turn;
    public Unit activeUnit;
    public Unit enemyUnit;
    public PlayerSO activePlayer;
    public PlayerSO enemyPlayer;
    public GameTableSO gameTable;
    public DataTablesSO dataTable;
}

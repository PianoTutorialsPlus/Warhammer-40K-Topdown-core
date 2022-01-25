using UnityEngine;

[CreateAssetMenu(menuName = "Game/Essential stats")]
public class GameStatsSO : ScriptableObject
{
    public GamePhase phase;
    public MovementPhase movementSubPhase;
    public int turn;
    public Unit activeUnit;
    public PlayerSO activePlayer;
    public PlayerSO enemyPlayer;
    public GameTableSO gameTable;

}

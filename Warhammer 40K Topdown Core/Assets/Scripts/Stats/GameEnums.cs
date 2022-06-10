namespace WH40K.Stats
{
    public enum GamePhase { MovementPhase, ShootingPhase }
    public enum MovementPhase { None = 0, Selection, Move, Next }
    public enum ShootingPhase { None = 0, Selection, Shoot, Next }
    public enum ShootingSubEvents { None = 0, SelectEnemy, Hit, Wound, Save, Damage }

}

using System;
using WH40K.Gameplay.PlayerEvents;
using WH40K.NavMesh;
using WH40K.Stats;
using WH40K.Stats.Player;
using Zenject;

namespace WH40K.Installers
{
    // Uncomment if you want to add alternative game settings
    //[CreateAssetMenu(menuName = "Game Settings")]
    class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public PlayerSettings Player;
        public NavMeshSettings NavMesh;

        [Serializable]
        public class PlayerSettings
        {
            public UnitStats.Settings UnitStatsHandler;
            public UnitMovementPhase.Settings MovementPhaseHandler;
            public UnitShootingPhase.Settings ShootingPhaseHandler; 
        }
        [Serializable]
        public class NavMeshSettings
        {
            public PathCalculator.Settings PathCalculatorHandler;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Player.UnitStatsHandler).IfNotBound();
            Container.BindInstance(Player.MovementPhaseHandler).IfNotBound();
            Container.BindInstance(Player.ShootingPhaseHandler).IfNotBound();

            Container.BindInstance(NavMesh.PathCalculatorHandler).IfNotBound();
        }

    }
}

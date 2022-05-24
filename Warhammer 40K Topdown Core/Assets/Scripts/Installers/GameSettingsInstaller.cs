using System;
using WH40K.NavMesh;
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

        }
        [Serializable]
        public class NavMeshSettings
        {
            public PathCalculator.Settings PathCalculatorHandler;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(NavMesh.PathCalculatorHandler).IfNotBound();
        }
    }
}

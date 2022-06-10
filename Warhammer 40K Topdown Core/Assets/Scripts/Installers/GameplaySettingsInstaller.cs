using System;
using UnityEngine;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;
using WH40K.UI;
using Zenject;

namespace WH40K.Installers
{

    // Uncomment if you want to add alternative game settings
    //[CreateAssetMenu(menuName = "Game Settings")]
    public class GameplaySettingsInstaller : ScriptableObjectInstaller<GameplaySettingsInstaller>
    {
        public GameStatsSettings GameStats;
        public GamePhaseSettings GamePhase;
        public UISettings UI;

        [Serializable]
        public class GamePhaseSettings
        {
            public MovementPhaseManager.Settings MovementPhaseHandler;
            public ShootingPhaseManager.Settings ShootingPhaseHandler;
        }
        [Serializable]
        public class UISettings
        {
            public UIRangeController.Settings RangeControllerHandler;
        }
        [Serializable]
        public class GameStatsSettings
        {
            public GameStatsSO.Settings GameStatsHandler;
        }
        public override void InstallBindings()
        {
            Container.BindInstance(GamePhase.MovementPhaseHandler).IfNotBound();
            Container.BindInstance(GamePhase.ShootingPhaseHandler).IfNotBound();

            Container.BindInstance(UI.RangeControllerHandler).IfNotBound();
            Container.BindInstance(GameStats.GameStatsHandler).NonLazy().IfNotBound();
        }
    }
}
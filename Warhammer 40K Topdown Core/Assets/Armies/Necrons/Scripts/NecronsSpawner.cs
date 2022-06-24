using System;
using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats.Player;

namespace WH40K.Armies.Necrons
{
    public class NecronsSpawner : UnitSpawner
    {
        protected GameObject NecronWarriorPrefab => _settings.Prefab1;


        public NecronsSpawner(Settings settings, UnitFacade.Factory factory) : base(settings, factory)
        {
        }

        public override void Initialize()
        {
            SpawnUnit(NecronWarriorPrefab);
        }

        public override void SpawnUnit(GameObject prefab)
        {
            var enemyFacade = CreatePrefab(prefab);
            enemyFacade.CurrentPosition = ChooseRandomStartPosition(5);
            _settings.Player.PlayerUnits.Add(enemyFacade.gameObject);
            _settings.Player.Fraction = Fraction.Necrons;
        }

        //[Serializable]
        //public class NecronsSettings : Settings
        //{
        //    //public PlayerSO Player1;

        //    public GameObject NecronWarriorPrefab;
        //}
    }
}

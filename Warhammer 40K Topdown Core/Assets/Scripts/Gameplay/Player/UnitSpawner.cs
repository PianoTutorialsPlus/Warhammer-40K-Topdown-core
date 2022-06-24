using System;
using UnityEngine;
using WH40K.Stats.Player;
using Zenject;
using Random = UnityEngine.Random;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitSpawner : IInitializable
    {
        protected readonly Settings _settings;
        protected readonly UnitFacade.Factory _unitFactory;

        public UnitSpawner(
            Settings settings,
            UnitFacade.Factory factory)
        {
            _settings = settings;
            _unitFactory = factory;
        }

        public virtual void Initialize()
        {
            SpawnUnit(_settings.Prefab1);
        }
        public virtual void SpawnUnit(GameObject prefab)
        {
            var enemyFacade = CreatePrefab(prefab);
            enemyFacade.CurrentPosition = ChooseRandomStartPosition(5);
            _settings.Player.PlayerUnits.Add(enemyFacade.gameObject);
            _settings.Player.Fraction = _settings.Fraction;
        }

        protected UnitFacade CreatePrefab(GameObject prefab)
        {
            return _unitFactory.Create(prefab);
        }

        protected Vector3 ChooseRandomStartPosition(int range)
        {
            var x_Pos = Random.Range(0, range);
            var z_Pos = Random.Range(0, range);
            var y_Pos = 1;

            return new Vector3(x_Pos, y_Pos, z_Pos);
        }

        [Serializable]
        public class Settings
        {
            public PlayerSO Player;
            public GameObject Prefab1;
            public Fraction Fraction;
        }
    }
}

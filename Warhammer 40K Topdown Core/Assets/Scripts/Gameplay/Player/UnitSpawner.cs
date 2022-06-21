using System;
using UnityEngine;
using WH40K.Stats.Player;
using Zenject;
using Random = UnityEngine.Random;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitSpawner : IInitializable
    {
        private readonly Settings _settings;
        private readonly UnitFacade.Factory _unitFactory;

        public UnitSpawner(
            Settings settings,
            UnitFacade.Factory factory)
        {
            _settings = settings;
            _unitFactory = factory;
        }

        public void Initialize()
        {
            SpawnPlayerUnit();
            SpawnEnemyUnit();
        }

        private void SpawnPlayerUnit()
        {
            var enemyFacade = _unitFactory.Create(Fraction.Necrons);
            enemyFacade.CurrentPosition = ChooseRandomStartPosition(5);
            _settings.Player1.PlayerUnits.Add(enemyFacade.gameObject);
            _settings.Player1.Fraction = Fraction.Necrons;
        }

        void SpawnEnemyUnit()
        {
            var enemyFacade = _unitFactory.Create(Fraction.SpaceMarines);
            enemyFacade.CurrentPosition = ChooseRandomStartPosition(-5);
            _settings.Player2.PlayerUnits.Add(enemyFacade.gameObject);
            _settings.Player2.Fraction = Fraction.SpaceMarines;
           
        }

        private Vector3 ChooseRandomStartPosition(int range)
        {
            var x_Pos = Random.Range(0, range);
            var z_Pos = Random.Range(0, range);
            var y_Pos = 1;

            return new Vector3(x_Pos, y_Pos, z_Pos);
        }

        [Serializable]
        public class Settings
        {
            public PlayerSO Player1;
            public PlayerSO Player2;
        }
    }
}

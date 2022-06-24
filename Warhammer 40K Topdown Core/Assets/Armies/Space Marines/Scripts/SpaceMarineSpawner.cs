using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats.Player;

namespace WH40K.Armies.SpaceMarines
{
    public class SpaceMarineSpawner : UnitSpawner
    {
        protected GameObject TacticalSquadPrefab => _settings.Prefab1;

        public SpaceMarineSpawner(Settings settings, UnitFacade.Factory factory) : base(settings, factory)
        {
        }

        public override void Initialize()
        {
            SpawnUnit(TacticalSquadPrefab);
        }

        public override void SpawnUnit(GameObject prefab)
        {
            var enemyFacade = CreatePrefab(prefab);
            enemyFacade.CurrentPosition = ChooseRandomStartPosition(5);
            _settings.Player.PlayerUnits.Add(enemyFacade.gameObject);
            _settings.Player.Fraction = Fraction.SpaceMarines;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WH40K.Stats.Player;
using Zenject;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitFactory : IFactory<Fraction,UnitFacade>
    {
        protected DiContainer _container;
        protected Settings _settings;

        public UnitFactory(DiContainer container, Settings settings)
        {
            _container = container;
            _settings = settings;
        }

        public UnitFacade Create(Fraction fraction)
        {
            UnitFacade unit;

            switch (fraction)
            {
                case Fraction.SpaceMarines:
                    unit = _container.InstantiatePrefab(_settings.spaceMarinePrefab).GetComponent<UnitFacade>();
                    break;
                case Fraction.Necrons:
                    unit = _container.InstantiatePrefab(_settings.necronWarriorPrefab).GetComponent<UnitFacade>();
                    break;
                default:
                    throw new ArgumentException("Unknown unit type");
            }
            return unit;
            
        }
        [Serializable]
        public class Settings
        {
            public GameObject spaceMarinePrefab;
            public GameObject necronWarriorPrefab;
        }
    }
}

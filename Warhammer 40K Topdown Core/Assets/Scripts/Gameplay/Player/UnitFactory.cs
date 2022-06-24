using UnityEngine;
using Zenject;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitFactory : IFactory<GameObject,UnitFacade>
    {
        protected DiContainer _container;

        public UnitFactory(DiContainer container)
        {
            _container = container;
        }

        public UnitFacade Create(GameObject prefab)
        {
            return _container.InstantiatePrefabForComponent<UnitFacade>(prefab);
        }
    }
}

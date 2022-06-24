using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using Zenject;

namespace WH40K.Armies.Necrons
{
    public class NecronsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("AddOn");
            Container.BindFactory<GameObject, UnitFacade, UnitFacade.Factory>().FromFactory<PrefabFactory<UnitFacade>>();
            Container.BindInterfacesAndSelfTo<UnitSpawner>().AsSingle();
        }
    }
}
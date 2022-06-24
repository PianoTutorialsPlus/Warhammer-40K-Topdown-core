using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using Zenject;

namespace WH40K.Installers
{
    public class AddOnInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<GameObject, UnitFacade, UnitFacade.Factory>().FromFactory<PrefabFactory<UnitFacade>>();
            Container.BindInterfacesAndSelfTo<UnitSpawner>().AsSingle();
        }
    }
}
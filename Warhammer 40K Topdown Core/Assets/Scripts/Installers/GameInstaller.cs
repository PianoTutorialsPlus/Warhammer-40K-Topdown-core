using System;
using UnityEngine;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;
using WH40K.UI;
using Zenject;

namespace WH40K.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallGamePhases();
        }

        private void InstallGamePhases()
        {
            Container.Bind<GamePhaseProcessor>().AsSingle().NonLazy();
            Container.Bind<MovementPhaseProcessor>().AsSingle().NonLazy();
            Container.Bind<ShootingPhaseProcessor>().AsSingle().NonLazy();
            Container.Bind<ShootingSubPhaseProcessor>().AsSingle().NonLazy();

            Container.Bind<UIRangeController>().AsSingle();
            //Container.BindFactory<Type, MovementPhases, MovementPhases.Factory>().FromFactory<GamePhaseFactory>().NonLazy();
            Container.BindInterfacesAndSelfTo<GamePhaseFactory>().AsSingle();
            //Container.Bind<MovementPhases>().To(x => x.AllNonAbstractClasses()).AsTransient();
        }
    }
}
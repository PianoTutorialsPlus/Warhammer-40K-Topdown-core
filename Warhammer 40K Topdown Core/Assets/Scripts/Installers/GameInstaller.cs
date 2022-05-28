using WH40K.GamePhaseEvents;
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
        }
    }
}
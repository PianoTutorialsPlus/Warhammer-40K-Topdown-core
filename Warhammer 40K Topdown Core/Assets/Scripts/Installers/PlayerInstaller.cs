using System;
using UnityEngine;
using UnityEngine.AI;
using WH40K.NavMesh;
using WH40K.PlayerEvents;
using Zenject;

namespace WH40K
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;
        public override void InstallBindings()
        {
            InstallUnitBindings();
            InstallNavMeshBindings();
            InstallPhaseBindings();
        }
        private void InstallUnitBindings()
        {
            Container.Bind<UnitModel>().AsSingle()
                .WithArguments(_settings.MeshRenderer, _settings.Rigidbody, _settings.Agent);

            Container.BindInterfacesAndSelfTo<MovementRange>().AsSingle()
                .WithArguments((float)_settings.UnitSO.Movement);

            Container.Bind<UnitMovementController>().AsSingle();
            Container.Bind<UnitSelector>().AsSingle();
        }
        private void InstallNavMeshBindings()
        {
            Container.Bind<NavMeshPath>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathCalculator>().AsSingle();
        }
        private void InstallPhaseBindings()
        {
            Container.Bind<UnitMovementPhase>().FromNewComponentOn(_settings.Unit).AsSingle();
            Container.Bind<UnitShootingPhase>().FromNewComponentOn(_settings.Unit).AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public MeshRenderer MeshRenderer;
            public Rigidbody Rigidbody;
            public NavMeshAgent Agent;
            public GameObject Unit;

            public UnitSO UnitSO;
            public WeaponSO WeaponSO;
        }
    }
}

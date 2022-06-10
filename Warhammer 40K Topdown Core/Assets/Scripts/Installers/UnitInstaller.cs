using System;
using UnityEngine;
using UnityEngine.AI;
using WH40K.Gameplay.PlayerEvents;
using WH40K.NavMesh;
using WH40K.Stats.Player;
using Zenject;

namespace WH40K.Installers
{
    public class UnitInstaller : MonoInstaller
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
                .WithArguments(
                    _settings.MeshRenderer, 
                    _settings.Rigidbody, 
                    _settings.Transform,
                    _settings.Agent);

            Container.BindInstance(_settings.UnitSO).AsSingle();
            Container.BindInstance(_settings.WeaponSO).AsSingle();

            Container.BindInterfacesAndSelfTo<MovementRange>().AsSingle()
                .WithArguments((float)_settings.UnitSO.Movement);

            Container.Bind<UnitMovementController>().AsSingle();
            Container.Bind<UnitSelector>().AsSingle();
            Container.Bind<UnitPointer>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitStats>().AsSingle();
        }
        private void InstallNavMeshBindings()
        {
            Container.Bind<NavMeshPath>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathCalculator>().AsSingle();
        }
        private void InstallPhaseBindings()
        {
            Container.BindInstance(_settings.Agent).AsSingle();

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
            public Transform Transform;

            public UnitSO UnitSO;
            public WeaponSO WeaponSO;
        }
    }
}

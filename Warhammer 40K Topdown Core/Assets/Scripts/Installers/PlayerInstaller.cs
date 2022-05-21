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
            Container.Bind<UnitModel>().AsSingle()
                .WithArguments(_settings.MeshRenderer, _settings.Agent);

            Container.Bind<NavMeshPath>().AsSingle();
            Container.Bind<PathCalculator>().AsSingle();
            //Container.Bind<UnitSelector>().AsSingle().NonLazy();
            //Container.BindInterfacesAndSelfTo<Unit>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public MeshRenderer MeshRenderer;
            public NavMeshAgent Agent;
        }
    }
}

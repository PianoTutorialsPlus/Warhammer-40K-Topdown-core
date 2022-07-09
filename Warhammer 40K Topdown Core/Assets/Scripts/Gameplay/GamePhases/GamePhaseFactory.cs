using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zenject;

namespace WH40K.Gameplay.GamePhaseEvents
{
    public class GamePhaseFactory
    {
        protected DiContainer _container;

        public GamePhaseFactory(DiContainer container)
        {
            _container = container;
        }

        public Dictionary<V, T> Create<V, T>(Dictionary<V, T> movementPhases) 
            where V : Enum
            where T : PhasesBase
        {
            movementPhases.Clear();
            var allShootingPhases = Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var subphase in allShootingPhases)
            {
                T Phases = Activator.CreateInstance(subphase) as T;
                
                _container.Bind<T>().WithId(subphase.ToString()).FromInstance(Phases);
                _container.QueueForInject(Phases);
                Phases = _container.ResolveId<T>(subphase.ToString());

                movementPhases.Add((V)Phases.SubEvents, Phases);
            }

            return movementPhases;
        }

    }
}

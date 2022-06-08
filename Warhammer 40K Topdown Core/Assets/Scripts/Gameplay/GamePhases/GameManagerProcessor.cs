//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using WH40K.GamePhaseEvents;

//namespace WH40K.Gameplay.GamePhaseEvents
//{
//    public class GameManagerProcessor
//    {
//        private static Dictionary<GamePhase, PhaseManagerBase> _gamePhaseManagers = new Dictionary<GamePhase, PhaseManagerBase>();
//        public static bool _initialized = false;

//        private void InitializeManager()
//        {
//            if (_initialized) return;
//            _gamePhaseManagers.Clear();

//            var allPhases = Assembly.GetAssembly(typeof(PhaseManagerBase)).GetTypes()
//                .Where(t => typeof(PhaseManagerBase).IsAssignableFrom(t) && t.IsAbstract == false);

//            foreach (var subphase in allPhases)
//            {
//                PhaseManagerBase instance = gameObject.GetComponentInChildren(subphase) as PhaseManagerBase;
//                _gamePhaseManagers.Add(instance.SubEvents, instance);
//            }
//            //_gamePhaseManagers = _gamePhases.ToDictionary(key => key.SubEvents, value => value);
//        }
//    }
//}

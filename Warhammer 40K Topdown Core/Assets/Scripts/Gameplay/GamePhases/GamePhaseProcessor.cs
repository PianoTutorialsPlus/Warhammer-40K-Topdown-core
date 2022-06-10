using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{

    /// <summary>
    /// This script processes the communication between the interaction manager and the main game phases executables.
    /// </summary>
    public class GamePhaseProcessor
    {
        // Variables
        private static Dictionary<GamePhase, GamePhases> _gamePhases = new Dictionary<GamePhase, GamePhases>();
        private static Dictionary<GamePhase, PhaseManagerBase> _gamePhaseManagers => _interactionManager.GamePhaseManagers;

        private static bool _initialized;
        private static GameStatsSO _gameStats;
        private static IInteractionManager _interactionManager;

        public bool Initialized { get => _initialized; protected set => _initialized = value; }

        public GamePhaseProcessor(GameStatsSO gameStats, IInteractionManager interactionManager)
        {
            _gameStats = gameStats;
            _interactionManager = interactionManager;
        }

        private static void Initialize()
        {
            if (_initialized) return;
            _gamePhases.Clear();

            var allPhases = Assembly.GetAssembly(typeof(GamePhases)).GetTypes()
                .Where(t => typeof(GamePhases).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var subphase in allPhases)
            {
                Debug.Log(_gameStats);
                GamePhases gamePhases = Activator.CreateInstance(subphase, args:_gameStats) as GamePhases;
                _gamePhases.Add(gamePhases.SubEvents, gamePhases);
            }

            _initialized = true;
        }

        public static void EnableNextPhase(GamePhase subPhase)
        {
            Initialize();

            var gamePhase = _gamePhases[subPhase];
            var gamePhaseManager = _gamePhaseManagers[subPhase];

            gamePhase.EnableNextPhase(gamePhaseManager);
        }

        public static void ResetPreviousPhase(GamePhase subPhase)
        {
            Initialize();

            var gamePhase = _gamePhases[subPhase];
            var gamePhaseManager = _gamePhaseManagers[subPhase];

            gamePhase.ResetPreviousPhase(gamePhaseManager);
        }

        public static void SetActivePlayerUnits(GamePhase subPhase)
        {
            Initialize();

            var gamePhase = _gamePhases[subPhase];
            gamePhase.SetActivePlayerUnits();
        }

        public static bool IsEndOfPlayerTurn(GamePhase subPhase)
        {
            Initialize();

            var gamePhase = _gamePhases[subPhase];
            return gamePhase.IsEndOfPlayerTurn();
        }

        internal static IEnumerable<GamePhase> GetAbilityByName()
        {
            Initialize();

            return _gamePhases.Keys;
        }
    }
}
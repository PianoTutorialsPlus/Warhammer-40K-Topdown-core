using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WH40K.Stats.Player
{
    [CreateAssetMenu(menuName = "Game/Player")]
    public class PlayerSO : ScriptableObject
    {
        private Settings _settings;

        public List<GameObject> _playerUnits;
        public Fraction _fraction;
        public Fraction Fraction { get => _fraction; set => _fraction = value; }
        public List<GameObject> PlayerUnits { get => _playerUnits; set => _playerUnits = value; }

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
            _playerUnits = _settings.PlayerUnits;
            _fraction = _settings.Fraction;
        }

        [Serializable]
        public class Settings
        {
            public Fraction Fraction;
            public List<GameObject> PlayerUnits;
        }
    }
}
using System;
using UnityEngine;
using Zenject;

namespace WH40K.Stats
{
    [CreateAssetMenu(menuName = "Game/Table")]
    public class GameTableSO : ScriptableObject
    {
        private Settings _settings;
        public GameObject GameTable;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
            GameTable = _settings.GameTable;
        }

        [Serializable]
        public class Settings
        {
            public GameObject GameTable;
        }
    }
}
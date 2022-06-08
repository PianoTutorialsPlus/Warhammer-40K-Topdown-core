using System.Collections.Generic;
using UnityEngine;
using WH40K.Stats.Player;

namespace WH40K.Gameplay.PlayerEvents
{
    [CreateAssetMenu(menuName = "Game/Player")]
    public class PlayerSO : ScriptableObject, IPlayer
    {
        private List<UnitFacade> _playerUnits;
        public Fraction _fraction;
        public Fraction Fraction { get => _fraction; set => _fraction = value; }
        public List<UnitFacade> PlayerUnits { get => _playerUnits; set => _playerUnits = value; }
    }
}
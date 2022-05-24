using System.Collections.Generic;
using UnityEngine;

namespace WH40K.PlayerEvents
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
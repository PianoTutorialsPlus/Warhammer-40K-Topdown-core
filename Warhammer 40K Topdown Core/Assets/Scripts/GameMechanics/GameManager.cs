using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace WH40K
{
    public class GameManager : MonoBehaviour
    {
        public Unit[] player1;
        public Unit[] player2;
        //public Text turnText;
        //public Text[] infoPanel;
        public PlayerSO _player1;
        public PlayerSO _player2;
        public GameTableSO _table;

        public Unit[] player;
        public GameObject userControl;
        //public string phase;
        public GameTable _gameTable;
        //private UnitManager unitManager;
        
        // Start is called before the first frame update
        void Start()
        {
            //unitManager = GetComponent<UnitManager>();
            //unitManager.Load();

            //_table.gameTable = gameTable;
            GameStats.GameTable = _table;
            GameStats.GameTable.gameTable = _gameTable;
            _player1.PlayerUnits.Clear();
            _player2.PlayerUnits.Clear();

            foreach (Unit unit in player1)
            {
                _player1.PlayerUnits.Add(unit);
            }

            foreach (Unit unit in player2)
            {
                _player2.PlayerUnits.Add(unit);
            }
            _player1._fraction = _player1.PlayerUnits[0].Fraction;
            _player2._fraction = _player2.PlayerUnits[0].Fraction;
            player = player1;
        }
    }
}

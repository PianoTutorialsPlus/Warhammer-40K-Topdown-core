using UnityEngine;
using WH40K.Gameplay.Core;
using WH40K.Gameplay.PlayerEvents;

namespace WH40K.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public UnitFacade[] player1;
        public UnitFacade[] player2;
        //public Text turnText;
        //public Text[] infoPanel;
        public PlayerSO _player1;
        public PlayerSO _player2;
        public GameTableSO _table;

        public UnitFacade[] player;
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

            foreach (UnitFacade unit in player1)
            {
                _player1.PlayerUnits.Add(unit);
            }

            foreach (UnitFacade unit in player2)
            {
                _player2.PlayerUnits.Add(unit);
            }
            _player1._fraction = _player1.PlayerUnits[0].Fraction;
            _player2._fraction = _player2.PlayerUnits[0].Fraction;
            player = player1;
        }
    }
}

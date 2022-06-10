using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats;
using WH40K.Stats.Player;

namespace WH40K.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] player1;
        public GameObject[] player2;
        //public Text turnText;
        //public Text[] infoPanel;
        public PlayerSO _player1;
        public PlayerSO _player2;
        public GameTableSO _table;

        public GameObject[] player;
        public GameObject userControl;
        //public string phase;
        public GameObject _gameTable;
        //private UnitManager unitManager;

        // Start is called before the first frame update
        void Start()
        {
            //unitManager = GetComponent<UnitManager>();
            //unitManager.Load();

            //_table.gameTable = gameTable;
            //GameStatsSO.GameTable = _table;
            //GameStatsSO.GameTable.gameTable = _gameTable;
            _player1.PlayerUnits.Clear();
            _player2.PlayerUnits.Clear();

            foreach (GameObject unit in player1)
            {
                _player1.PlayerUnits.Add(unit);
            }

            foreach (GameObject unit in player2)
            {
                _player2.PlayerUnits.Add(unit);
            }
            var playerUnits = _player1.PlayerUnits[0].GetComponent<UnitFacade>();
            var enemyUnits = _player2.PlayerUnits[0].GetComponent<UnitFacade>();
            _player1._fraction = playerUnits.Fraction;
            _player2._fraction = enemyUnits.Fraction;
            player = player1;
        }
    }
}
